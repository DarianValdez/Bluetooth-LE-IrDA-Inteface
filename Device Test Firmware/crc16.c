#include "crc16.h"


unsigned int crc16( unsigned char *p, unsigned int n )
{
    unsigned char  i;
    unsigned short crc = 0xFFFF;

    while (n--)
    {
        crc ^= *p++;
        for (i = 8; i != 0; i--)
        {
            if (crc & 1)
                crc = (crc >> 1) ^ POLY;
            else
                crc >>= 1;
        }
    }
    return (crc);
}