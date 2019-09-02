const int lockPin = 8;
const int sensorPin = 2;

int sensorState = 0;

void setup() {
  pinMode(lockPin, OUTPUT); //Sets output pin for lock relay
  pinMode(sensorPin, INPUT);  //Sets input pin for tag sensor
  Serial.begin(9600);
}


void loop() 
{
  //Inıtial state is LOW for lockpin
  digitalWrite(lockPin,LOW);
  
  //Listen Serial Port
  if(Serial.available())
  {
    char c = Serial.read(); //Write serial prot data to c variable*/
    //char c = '1';
    if(c == '1')
    {
      delay(100);
      while(true){   
        if(digitalRead(sensorPin) ==LOW){
          digitalWrite(lockPin,HIGH);
          delay(1000);
          digitalWrite(lockPin,LOW);
          Serial.write("1");
          break;
          }          
       }
       
     }
  }
}
