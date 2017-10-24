#include "stdbool.h"
#include "commonDefines.h"
#include "cmd_def.h"
#include "main.h"
#include "ble112.h"

int receivedBytes = 0;
struct ble_header header;
const struct ble_msg * messagePtr;
uint8 data[30];
extern bool respWait;
bool wakupEvtHandled = false;
uint8 dataLen;

#pragma vector=USCI_A0_VECTOR
__interrupt void USCI_A0_ISR(void)
{ 
  switch(receivedBytes)
  {
     case 0:
        header.type_hilen = UCA0RXBUF;
        receivedBytes++;
        break;
     case 1:
        header.lolen = UCA0RXBUF;
        receivedBytes++;
        if(respWait)
        {
          //WAKUP_PORT &= ~WAKUP_PIN;
        }
        dataLen = header.lolen + 4;
        break;
     case 2:
        header.cls = UCA0RXBUF;
        receivedBytes++;
        break;
     case 3:
        header.command = UCA0RXBUF;
        receivedBytes++;
        messagePtr = ble_get_msg_hdr(header);
       
        break;
     default:
       data[(receivedBytes - 4)] = UCA0RXBUF;
       if(receivedBytes == 11 && *messagePtr->handler == (ble_cmd_handler)ble_evt_attributes_value)
        {
          dataLen += data[6];
        }
       receivedBytes++;
       break;       
   }
  
  if(receivedBytes == dataLen)
  {
      (*messagePtr->handler)(data);
      if(respWait)
      {
        respWait = false;
      }
      clearBuf(data, sizeof(data));
      receivedBytes = 0;
  }
  UCA0IFG &= ~UCRXIFG;
}