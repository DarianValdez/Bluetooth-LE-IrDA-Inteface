#ifndef COMMON_DEF_
#define COMMON_DEF_

#include "msp430x54x.h"

#define UART_PORT     P3DIR
#define WAKUP_PORT    P5OUT
#define WAKUP_PIN     BIT0
#define TX_PIN        BIT4
#define RX_PIN        BIT5

#define CMDDATA_HDR   0x08
#define RESPDATA_HDR   0x0B

#endif