
#include "cmd_def.h"
#include "stdbool.h"
#include "ByteOrders.h"
#include "ble112.h"
#include "commonDefines.h"

#include <string.h>
#include "main.h"

int i = 0;

void main( void )
{
  
  // Stop watchdog timer to prevent time out reset
  WDTCTL = WDTPW + WDTHOLD;
  
  __bis_SR_register(SCG0);
  
  UCSCTL2 &= FLLN0+FLLN1+FLLN2+FLLN3;
  UCSCTL2 |= FLLD__8;
  UCSCTL3 = SELREF__REFOCLK + FLLREFDIV_0;
  UCSCTL4 = SELM__DCOCLK + SELS__DCOCLK + SELA__REFOCLK;
  
  __bic_SR_register(SCG0);
  
  for(i = 0; i < 0xfffe; i++)  //DCO Settle
    __delay_cycles(10);
  
  UCA0BR0 = 0x02;
  UCA0MCTL = UCOS16 + UCBRS_6 + UCBRF_3;
  UCA0CTL1 |= 0x80;
    
  UART_PORT |= TX_PIN;
  P3REN |= TX_PIN;
  P5DIR |= WAKUP_PIN;
  WAKUP_PORT &= ~WAKUP_PIN;
  
  P3SEL |= TX_PIN + RX_PIN;
  UCA0CTL1 &= ~UCSWRST;
  UCA0IE = UCRXIFG;
   __bis_SR_register(GIE);
 
  WAKUP_PORT |= WAKUP_PIN;
  ble_cmd_gap_set_mode(gap_general_discoverable, gap_undirected_connectable);
  
  
  while(1)
  {
  }
}

void clearBuf(uint8 *buf, uint8 len)
{
  for(i = 0; i < len; i++)
  {
    buf[i] = 0x00;
  }
}

