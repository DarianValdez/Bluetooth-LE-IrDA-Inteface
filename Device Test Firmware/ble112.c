#include "cmd_def.h"
#include "ByteOrders.h"
#include "commonDefines.h"
#include "ble112.h"
#include "crc16.h"
#include "main.h"
#include <stdbool.h>
#include <string.h>

bool respWait = false;
extern bool wakupEvtHandled;
uint8 packetBuf[13];
uint8 cmdLen = 0;
uint8 bytesReceived;
uint8 cmd[255];

void txCommand(struct ble_cmd_packet *commandPacket)
{
  /**
  Copy the contents of the struct into an array
  **/
  uint8 size = commandPacket->header.lolen + 5;
  packetBuf[0] = size - 1;
  memcpy(packetBuf + 1, &commandPacket->header, 4 + commandPacket->header.lolen);
  
  if(commandPacket->header.lolen > 0x00)
  {
    RevByteOrder(packetBuf + 5, commandPacket->header.lolen);
  }
  
  /**
  Wait for the module to wakeup
  **/
  while(!wakupEvtHandled)
  {}
  
  /**
  Transmit
  **/
  uint8 i = 0;   
  while( i <= size)
  {
      if(UCA0IFG&UCTXIFG)
      {         
        UCA0TXBUF = packetBuf[i];
        i++;       
      }
  }
  respWait = true;
}

void ble_default(uint8 *data)
{
}

void ble_rsp_system_reset(uint8 *data)
{
}

void ble_rsp_system_hello(uint8 *data)
{
}

void ble_rsp_system_address_get(uint8 *data)
{
}

void ble_rsp_system_reg_write(uint8 *data)
{
}

void ble_rsp_system_reg_read(uint8 *data)
{
}

void ble_rsp_system_get_counters(uint8 *data)
{
}

void ble_rsp_system_get_connections(uint8 *data)
{
}

void ble_rsp_system_read_memory(uint8 *data)
{
}

void ble_rsp_system_get_info(uint8 *data)
{
}

void ble_rsp_system_endpoint_tx(uint8 *data)
{
}

void ble_rsp_system_whitelist_append(uint8 *data)
{
}

void ble_rsp_system_whitelist_remove(uint8 *data)
{
}

void ble_rsp_system_whitelist_clear(uint8 *data)
{
}

void ble_rsp_system_endpoint_rx(uint8 *data)
{
}

void ble_rsp_system_endpoint_set_watermarks(uint8 *data)
{
}

void ble_rsp_system_aes_setkey(uint8 *data)
{
}

void ble_rsp_system_aes_encrypt(uint8 *data)
{
}

void ble_rsp_system_aes_decrypt(uint8 *data)
{
}

void ble_rsp_flash_ps_defrag(uint8 *data)
{
}

void ble_rsp_flash_ps_dump(uint8 *data)
{
}

void ble_rsp_flash_ps_erase_all(uint8 *data)
{
}

void ble_rsp_flash_ps_save(uint8 *data)
{
}

void ble_rsp_flash_ps_load(uint8 *data)
{
}

void ble_rsp_flash_ps_erase(uint8 *data)
{
}

void ble_rsp_flash_erase_page(uint8 *data)
{
}

void ble_rsp_flash_write_data(uint8 *data)
{
}

void ble_rsp_flash_read_data(uint8 *data)
{
}

void ble_rsp_attributes_write(uint8 *data)
{
}

void ble_rsp_attributes_read_type(uint8 *data)
{
}

void ble_rsp_attributes_user_read_response(uint8 *data)
{
}

void ble_rsp_attributes_user_write_response(uint8 *data)
{
}

void ble_rsp_attributes_send(uint8 *data)
{
}

void ble_rsp_connection_disconnect(uint8 *data)
{
}

void ble_rsp_connection_get_rssi(uint8 *data)
{
}

void ble_rsp_connection_update(uint8 *data)
{
}

void ble_rsp_connection_version_update(uint8 *data)
{
}

void ble_rsp_connection_channel_map_get(uint8 *data)
{
}

void ble_rsp_connection_channel_map_set(uint8 *data)
{
}

void ble_rsp_connection_features_get(uint8 *data)
{
}

void ble_rsp_connection_get_status(uint8 *data)
{
}

void ble_rsp_connection_raw_tx(uint8 *data)
{
}

void ble_rsp_connection_slave_latency_disable(uint8 *data)
{
}

void ble_rsp_attclient_find_by_type_value(uint8 *data)
{
}

void ble_rsp_attclient_read_by_group_type(uint8 *data)
{
}

void ble_rsp_attclient_read_by_type(uint8 *data)
{
}

void ble_rsp_attclient_find_information(uint8 *data)
{
}

void ble_rsp_attclient_read_by_handle(uint8 *data)
{
}

void ble_rsp_attclient_attribute_write(uint8 *data)
{
}

void ble_rsp_attclient_write_command(uint8 *data)
{
}

void ble_rsp_attclient_indicate_confirm(uint8 *data)
{
}

void ble_rsp_attclient_read_long(uint8 *data)
{
}

void ble_rsp_attclient_prepare_write(uint8 *data)
{
}

void ble_rsp_attclient_execute_write(uint8 *data)
{
}

void ble_rsp_attclient_read_multiple(uint8 *data)
{
}

void ble_rsp_sm_encrypt_start(uint8 *data)
{
}

void ble_rsp_sm_set_bondable_mode(uint8 *data)
{
}

void ble_rsp_sm_delete_bonding(uint8 *data)
{
}

void ble_rsp_sm_set_parameters(uint8 *data)
{
}

void ble_rsp_sm_passkey_entry(uint8 *data)
{
}

void ble_rsp_sm_get_bonds(uint8 *data)
{
}

void ble_rsp_sm_set_oob_data(uint8 *data)
{
}

void ble_rsp_sm_whitelist_bonds(uint8 *data)
{
}

void ble_rsp_gap_set_privacy_flags(uint8 *data)
{
}

void ble_rsp_gap_set_mode(uint8 *data)
{
}

void ble_rsp_gap_discover(uint8 *data)
{
}

void ble_rsp_gap_connect_direct(uint8 *data)
{
}

void ble_rsp_gap_end_procedure(uint8 *data)
{
}

void ble_rsp_gap_connect_selective(uint8 *data)
{
}

void ble_rsp_gap_set_filtering(uint8 *data)
{
}

void ble_rsp_gap_set_scan_parameters(uint8 *data)
{
}

void ble_rsp_gap_set_adv_parameters(uint8 *data)
{
}

void ble_rsp_gap_set_adv_data(uint8 *data)
{
}

void ble_rsp_gap_set_directed_connectable_mode(uint8 *data)
{
}

void ble_rsp_gap_set_initiating_con_parameters(uint8 *data)
{
}

void ble_rsp_hardware_io_port_config_irq(uint8 *data)
{
}

void ble_rsp_hardware_set_soft_timer(uint8 *data)
{
}

void ble_rsp_hardware_adc_read(uint8 *data)
{
}

void ble_rsp_hardware_io_port_config_direction(uint8 *data)
{
}

void ble_rsp_hardware_io_port_config_function(uint8 *data)
{
}

void ble_rsp_hardware_io_port_config_pull(uint8 *data)
{
}

void ble_rsp_hardware_io_port_write(uint8 *data)
{
}

void ble_rsp_hardware_io_port_read(uint8 *data)
{
}

void ble_rsp_hardware_spi_config(uint8 *data)
{
}

void ble_rsp_hardware_spi_transfer(uint8 *data)
{
}

void ble_rsp_hardware_i2c_read(uint8 *data)
{
}

void ble_rsp_hardware_i2c_write(uint8 *data)
{
}

void ble_rsp_hardware_set_txpower(uint8 *data)
{
}

void ble_rsp_hardware_timer_comparator(uint8 *data)
{
}

void ble_rsp_hardware_io_port_irq_enable(uint8 *data)
{
}

void ble_rsp_hardware_io_port_irq_direction(uint8 *data)
{
}

void ble_rsp_hardware_analog_comparator_enable(uint8 *data)
{
}

void ble_rsp_hardware_analog_comparator_read(uint8 *data)
{
}

void ble_rsp_hardware_analog_comparator_config_irq(uint8 *data)
{
}

void ble_rsp_hardware_set_rxgain(uint8 *data)
{
}

void ble_rsp_hardware_usb_enable(uint8 *data)
{
}

void ble_rsp_test_phy_tx(uint8 *data)
{
}

void ble_rsp_test_phy_rx(uint8 *data)
{
}

void ble_rsp_test_phy_end(uint8 *data)
{
}

void ble_rsp_test_phy_reset(uint8 *data)
{
}

void ble_rsp_test_get_channel_map(uint8 *data)
{
}

void ble_rsp_test_debug(uint8 *data)
{
}

void ble_rsp_test_channel_mode(uint8 *data)
{
}

void ble_rsp_dfu_reset(uint8 *data)
{
}

void ble_rsp_dfu_flash_set_address(uint8 *data)
{
}

void ble_rsp_dfu_flash_upload(uint8 *data)
{
}

void ble_rsp_dfu_flash_upload_finish(uint8 *data)
{
}

void ble_evt_system_boot(uint8 *data)
{
}

void ble_evt_system_debug(uint8 *data)
{
}

void ble_evt_system_endpoint_watermark_rx(uint8 *data)
{
}

void ble_evt_system_endpoint_watermark_tx(uint8 *data)
{
}

void ble_evt_system_script_failure(uint8 *data)
{
}

void ble_evt_system_no_license_key(uint8 *data)
{
}

void ble_evt_system_protocol_error(uint8 *data)
{
}

void ble_evt_flash_ps_key(uint8 *data)
{
}

void ble_evt_attributes_value(uint8 *data)
{
/**
  When the event is received, the handle must equal the handle of charCommandData in GATT.
  Byte 1 of value_data is the total length of the entire command. value_data is copied to cmd
  If the total length is greater than 19, then the data is sent from the client in multiple
  chunks, and offset in memcpy(). The maximum payload of any ble packet is 20 bytes
  
  Event structure:
  uint8 connection,
  uint8 reason,
  uint16 handle,
  uint16 offset,
  uint8 value_len,
  const uint8* value_data
**/
  uint8 handle = data[2];
  uint8 offset = data[4];
  uint8 packetLen = data[6] - 1;
  bytesReceived += data[6] - 1;
  
  if(cmdLen == 0)
    cmdLen = data[7];
  
  
  if(handle == CMDDATA_HDR)
  {
    memcpy(cmd + offset, data + 9, packetLen);
  }
  
  if(bytesReceived == cmdLen)
  {
    if((cmd[cmdLen - 2] << 8) | cmd[cmdLen - 1] == crc16(cmd, cmdLen))
    {
      //TxDataIrda(cmd, cmdLen);
    }
    cmdLen = 0;
    clearBuf(cmd, cmdLen);
  }
}

void ble_rsp_attributes_read(uint8 *data)
{
  /**
  uint16 handle,
  uint16 offset,
  uint16 result,
  uint8 value_len,
  const uint8* value_data
  **/  
}

void ble_evt_attributes_user_read_request(uint8 *data)
{
}

void ble_evt_attributes_status(uint8 *data)
{
}

void ble_evt_connection_status(uint8 *data)
{
  //0 0x80 hilen Message type: event
  //1 0x10 lolen Minimum payload length
  //2 0x03 class Message class: Connection
  //3 0x00 method Message ID
  //4 uint8 connection Connection handle
  //5 uint8 flags Connection status flags use connstatus-enumerator
  //6 - 11 bd_addr address Remote devices Bluetooth address
  //12 uint8 address_type Remote address type see: Bluetooth Address Types--gap
  //13 - 14 uint16 conn_interval Current connection interval (units of 1.25ms)
  //15 - 16 uint16 timeout Current supervision timeout (units of 10ms)
  //17 - 18 uint16 latency Slave latency which tells how many connection intervals the slave may
  //skip.
  //19 uint8 bonding Bonding handle if the device has been bonded with  
}

void ble_evt_connection_version_ind(uint8 *data)
{
}

void ble_evt_connection_feature_ind(uint8 *data)
{
}

void ble_evt_connection_raw_rx(uint8 *data)
{
}

void ble_evt_connection_disconnected(uint8 *data)
{
}

void ble_evt_attclient_indicated(uint8 *data)
{
}

void ble_evt_attclient_procedure_completed(uint8 *data)
{
}

void ble_evt_attclient_group_found(uint8 *data)
{
}

void ble_evt_attclient_attribute_found(uint8 *data)
{
}

void ble_evt_attclient_find_information_found(uint8 *data)
{
}

void ble_evt_attclient_attribute_value(uint8 *data)
{
}

void ble_evt_attclient_read_multiple_response(uint8 *data)
{
}

void ble_evt_sm_smp_data(uint8 *data)
{
}

void ble_evt_sm_bonding_fail(uint8 *data)
{
}

void ble_evt_sm_passkey_display(uint8 *data)
{
}

void ble_evt_sm_passkey_request(uint8 *data)
{
}

void ble_evt_sm_bond_status(uint8 *data)
{
}

void ble_evt_gap_scan_response(uint8 *data)
{
}

void ble_evt_gap_mode_changed(uint8 *data)
{
}

void ble_evt_hardware_io_port_status(uint8 *data)
{
  wakupEvtHandled = true;
}

void ble_evt_hardware_soft_timer(uint8 *data)
{
}

void ble_evt_hardware_adc_result(uint8 *data)
{
}

void ble_evt_hardware_analog_comparator_status(uint8 *data)
{
}

void ble_evt_dfu_boot(uint8 *data)
{
}

