#include <SPI.h>
#include <Ethernet.h>

// Endereço MAC do módulo Ethernet Shield
byte mac[] = {0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED};

IPAddress ip(192, 168, 8, 10);
// Inicializa o servidor na porta 80
EthernetServer server(80);

void setup() {
  // Inicializa o módulo Ethernet
  Ethernet.begin(mac, ip);
  server.begin();
  Serial.begin(9600);
  pinMode(8, OUTPUT);
}

void loop() {
  // Aguarda uma conexão
  EthernetClient client = server.available();
  digitalWrite(8, LOW);
  if (client) {
    while (client.connected()) {
      if (client.available()) {
        // Lê o comando recebido
        String command = client.readStringUntil('\r\n');
        
        if (command.equals("on")) {
          // Aciona o LED (pin 8 no Arduino Uno)
          digitalWrite(8, HIGH);
          Serial.println("OK");
          delay(5000);
        } else if (command.equals("off")) {
          // Desliga o LED
          digitalWrite(8, LOW);
          delay(5000);
        } else if(command.equals("end")) {
          // Fecha a conexão
          client.stop();
        }
      }
    }
  }
}