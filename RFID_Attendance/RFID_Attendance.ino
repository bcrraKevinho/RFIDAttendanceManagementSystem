#include <Arduino.h>
#include <SPI.h>
#include <MFRC522.h>
#include <LiquidCrystal.h>
#include <WiFi.h>
#include <esp_wifi.h>
#include <HTTPClient.h>
#include <ESP32Time.h>
#include <Arduino_JSON.h>

ESP32Time rtc;

//Pines para RFID y Display
#define SS_PIN 5
#define RST_PIN 22
LiquidCrystal lcd(17,16,4,0,2,15);

//======================================== SSID and Password of your WiFi router.
const char* ssid = "Your_SSDI";
const char* password = "Your_password";
//======================================== 

//======================================== Variables for HTTP POST request data.
String postData = ""; //--> Variables sent for HTTP POST request data.
String payload = "";  //--> Variable for receiving response from HTTP POST.
//======================================== 

//======================================== Datos Http
String HOST_NAME = "http://localhost/rfidAPI";
String PATH_NAME   = "/api/Asistencia/registro";
//======================================== 

//======================================== Servidor NTP
const char* ntpServer = "pool.ntp.org";
const long gmtOffset_sec = -7*3600;
const int daylightOffset_sec = 0;
//======================================== 

//======================================== Instancias RFID
MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;
//======================================== 

//======================================== Array to store a key
byte nuidPICC[4];
//======================================== 

String ClaveRFID, nombre, matricula, id;
int len;

void setup() 
{
  Serial.begin(115200);
  SPI.begin(); // Init SPI bus
  rfid.PCD_Init(); // Init MFRC522
  lcd.begin(16, 2); // Init LCD 16x2
  
  //Init WiFi
  WiFi.begin(ssid, password);
  int count = 0;
  String dot = "";
  while (WiFi.status() != WL_CONNECTED) {
    count++;
    dot+=".";
    if(count==3){
      count = 0;
      dot = ".";
    }
    delay(1000);
    lcd.clear();
    lcd.setCursor(3,0);
    lcd.print("Conectando");
    lcd.setCursor(3,1);
    lcd.print("a Wi-Fi"+dot);
  }

  //Time
  configTime(gmtOffset_sec, daylightOffset_sec, ntpServer);
  struct tm timeinfo;
  if(getLocalTime(&timeinfo))
    rtc.setTimeStruct(timeinfo);

  lcd.clear();
  lcd.setCursor(4,0);
  lcd.print("Conexion");
  lcd.setCursor(4,1);
  lcd.print("exitosa!");
  delay(3000);

  for (byte i = 0; i < 6; i++) {
    key.keyByte[i] = 0xFF;
  } 
  ClaveRFID = printHex(key.keyByte, MFRC522::MF_KEY_SIZE);
}

void loop() 
{
    lcd.clear();
    lcd.setCursor(8,0);
    lcd.print(rtc.getTime("%H:%M:%S"));
    lcd.setCursor(0,1);
    lcd.print("Listo "+rtc.getTime("%d/%m/%Y"));
   
    // Reset the loop if no new card present on the sensor/reader. This saves the entire process when idle.
    if ( ! rfid.PICC_IsNewCardPresent()){return;}
    // Verify if the NUID has been readed
    if ( ! rfid.PICC_ReadCardSerial()){return;}
    
    MFRC522::PICC_Type piccType = rfid.PICC_GetType(rfid.uid.sak);
    // Check is the PICC of Classic MIFARE type
    if (piccType != MFRC522::PICC_TYPE_MIFARE_MINI && piccType != MFRC522::PICC_TYPE_MIFARE_1K && piccType != MFRC522::PICC_TYPE_MIFARE_4K)
    {
      Serial.println("Su Tarjeta no es del tipo MIFARE Classic.");
      return;
    }
    
    Serial.println("Se ha detectado una nueva tarjeta.");
    
    lcd.clear();
    lcd.setCursor(3,0);
    lcd.print("Recuperando");
    lcd.setCursor(1,1);
    lcd.print("informacion...");

    ClaveRFID = printHex(rfid.uid.uidByte, rfid.uid.size);
    Serial.print("clave Tarjeta: "); Serial.println(ClaveRFID);

    //HTTP request
    HTTPClient http;
    postData = "{\"mac\": \""+ClaveRFID+"\", \"idsalon\": \""+WiFi.macAddress()+"\"}";
    payload = "";

    http.begin(HOST_NAME + PATH_NAME);
    http.addHeader("Content-Type", "application/json");        //--> Specify content-type header
    int httpResponseCode = http.POST(postData); //--> Send the request
    payload = http.getString();     //--> Get the response payload
    Serial.print("httpCode : ");
    Serial.println(httpResponseCode); //--> Print HTTP return code
    Serial.print("payload  : ");
    Serial.println(payload);  //--> Print request response payload
    http.end();  //--> Close connection

    JSONVar myObject = JSON.parse(payload);
    if (JSON.typeof(myObject) == "undefined") {
      Serial.println("Parsing input failed!");
      Serial.println("---------------");
      return;
    }

    if(payload == "\"Alumno no encontrado\""){
      lcd.clear();
      lcd.setCursor(5,0);
      lcd.print("Alumno");
      lcd.setCursor(1,1);
      lcd.print("no encontrado");
      delay(3000);
      return;
    }

    if (myObject.hasOwnProperty("nombre")) {
      nombre = JSON.stringify(myObject["nombre"]);
    }

    if (myObject.hasOwnProperty("matricula")) {
      matricula = JSON.stringify(myObject["matricula"]);
    }

    printInfoDisplay(nombre,matricula);
    Serial.println();


String printHex(byte *buffer, byte bufferSize)
{  
   String DatoHexAux = "";
   for (byte i = 0; i < bufferSize; i++) 
   {
       if (buffer[i] < 0x10)
       {
        DatoHexAux = DatoHexAux + "0";
        DatoHexAux = DatoHexAux + String(buffer[i], HEX);  
       }
       else { DatoHexAux = DatoHexAux + String(buffer[i], HEX); }
   }
   
   for (int i = 0; i < DatoHexAux.length(); i++) {DatoHexAux[i] = toupper(DatoHexAux[i]);}
   return DatoHexAux;
}

void printInfoDisplay(String name, String matricula)
{
    len = name.length();
    lcd.clear();
    for(int i=0;i<len+8;i++)
    {
      delay(200);
      lcd.setCursor(8,0);
      lcd.print(name);
      lcd.setCursor(8,1);
      lcd.print(matricula);
      lcd.scrollDisplayLeft();
    }
}
