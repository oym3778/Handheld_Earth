#include <Arduino_LSM6DS3.h>
#include <MadgwickAHRS.h>

// initialize a Madgwick filter:
Madgwick madgwick;
// sensor's sample rate is fixed at 104 Hz
// you can also get this info by calling accelerationSampleRate() or gyroscopeSampleRate()
const float sensorRate = 104.00;

const int preyPin = A0;
const int predPin = A1;

int preyState = 0;
int predState = 0;

void setup()
{
  Serial.begin(9600);
  // attempt to start the IMU:
  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU");
    // stop here if you can't access the IMU:
    while (true);
  }
  // start the filter to run at the sample rate:
  madgwick.begin(sensorRate);
  pinMode(preyPin, INPUT_PULLUP);
  pinMode(predPin, INPUT_PULLUP);
}

void loop()
{

  // values for acceleration and rotation:
  float xAcc, yAcc, zAcc;
  float xGyro, yGyro, zGyro;

  // values for orientation:
  float roll, pitch, yaw;
  // check if the IMU is ready to read:
  if (IMU.accelerationAvailable() && IMU.gyroscopeAvailable())
  {
    // read accelerometer &and gyrometer:
    IMU.readAcceleration(xAcc, yAcc, zAcc);
    IMU.readGyroscope(xGyro, yGyro, zGyro);

    // update the filter, which computes orientation:
    madgwick.updateIMU(xGyro, yGyro, zGyro, xAcc, yAcc, zAcc);

    // print the heading, pitch and roll
    roll = madgwick.getRoll();
    pitch = madgwick.getPitch();
    yaw = madgwick.getYaw();

    yaw = map(yaw, 0, 360, -180, 180);

    Serial.print(roll);
    Serial.print(",");
    Serial.print(pitch);
    Serial.print(",");
    Serial.print(yaw);

    preyState = digitalRead(preyPin);
    predState = digitalRead(predPin);

    Serial.print(",");
    Serial.print(preyState);
    Serial.print(",");
    Serial.println(predState);
    // short delay to keep things stable
    delay(1);
  } 
}