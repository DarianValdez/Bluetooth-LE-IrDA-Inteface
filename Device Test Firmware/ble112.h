#ifndef BLE112_H_
#define BLE112_H_

void txCommand(struct ble_cmd_packet *txPacket);

/**Hello - command for testing**/
void ble_rsp_system_hello(uint8 *data);

/**Get device bluetooth address**/
void ble_rsp_system_address_get(uint8 *data);

/**write register**/
void ble_rsp_system_reg_write(uint8 *data);

/**read register**/
void ble_rsp_system_reg_read(uint8 *data);

/**get and reset packet counters**/
void ble_rsp_system_get_counters(uint8 *data);

/**Get status from all connections**/
void ble_rsp_system_get_connections(uint8 *data);

/**Read Memory**/
void ble_rsp_system_read_memory(uint8 *data);

/**Get Device info**/
void ble_rsp_system_get_info(uint8 *data);

/**Send data to endpoint, error is returned if endpoint does not have enough space**/
void ble_rsp_system_endpoint_tx(uint8 *data);

/**Add entry to whitelist**/
void ble_rsp_system_whitelist_append(uint8 *data);

/**Remove entry from whitelist**/
void ble_rsp_system_whitelist_remove(uint8 *data);

/**Add entry to whitelist**/
void ble_rsp_system_whitelist_clear(uint8 *data);

/**Read data from endpoint, error is returned if endpoint does not have enough data.**/
void ble_rsp_system_endpoint_rx(uint8 *data);

/**Set watermarks on both input and output side**/
void ble_rsp_system_endpoint_set_watermarks(uint8 *data);

/**Set encryption key that will be used with aes_encrypt and aes_decrypt commands.**/
void ble_rsp_system_aes_setkey(uint8 *data);

/**Encrypt one block of data**/
void ble_rsp_system_aes_encrypt(uint8 *data);

/**Decrypt one block of data**/
void ble_rsp_system_aes_decrypt(uint8 *data);

/**Defragment persistent store**/
void ble_rsp_flash_ps_defrag(uint8 *data);

/**Dump all ps keys**/
void ble_rsp_flash_ps_dump(uint8 *data);

/**erase all ps keys**/
void ble_rsp_flash_ps_erase_all(uint8 *data);

/**save ps key**/
void ble_rsp_flash_ps_save(uint8 *data);

/**load ps key**/
void ble_rsp_flash_ps_load(uint8 *data);

/**erase ps key**/
void ble_rsp_flash_ps_erase(uint8 *data);

/**erase flash page**/
void ble_rsp_flash_erase_page(uint8 *data);

/**write data to user data area**/
void ble_rsp_flash_write_data(uint8 *data);

/**read data from user data area.**/
void ble_rsp_flash_read_data(uint8 *data);

/**Write to attribute database**/
void ble_rsp_attributes_write(uint8 *data);

/**Read from attribute database**/
void ble_rsp_attributes_read(uint8 *data);

/**Read attribute type from database**/
void ble_rsp_attributes_read_type(uint8 *data);

/**Respond to user attribute read request**/
void ble_rsp_attributes_user_read_response(uint8 *data);

/**Response to attribute_changed event where reason is user-attribute write.**/
void ble_rsp_attributes_user_write_response(uint8 *data);

/**Send notification or indication to remote device.**/
void ble_rsp_attributes_send(uint8 *data);

/**Disconnect connection, starts a disconnection procedure on connection**/
void ble_rsp_connection_disconnect(uint8 *data);

/**Get Link RSSI**/
void ble_rsp_connection_get_rssi(uint8 *data);

/**Update connection parameters**/
void ble_rsp_connection_update(uint8 *data);

/**Request version exchange**/
void ble_rsp_connection_version_update(uint8 *data);

/**Get Current channel map**/
void ble_rsp_connection_channel_map_get(uint8 *data);

/**Set Channel map**/
void ble_rsp_connection_channel_map_set(uint8 *data);

/**Remote feature request**/
void ble_rsp_connection_features_get(uint8 *data);

/**Get Connection Status Parameters**/
void ble_rsp_connection_get_status(uint8 *data);

/**Raw TX**/
void ble_rsp_connection_raw_tx(uint8 *data);

/**Disable or enable slave latency**/
void ble_rsp_connection_slave_latency_disable(uint8 *data);

/**Discover attributes by type and value**/
void ble_rsp_attclient_find_by_type_value(uint8 *data);

/**Discover attributes by type and value**/
void ble_rsp_attclient_read_by_group_type(uint8 *data);

/**Read all attributes where type matches**/
void ble_rsp_attclient_read_by_type(uint8 *data);

/**Discover Attribute handle and type mappings**/
void ble_rsp_attclient_find_information(uint8 *data);

/**Read Characteristic value using handle**/
void ble_rsp_attclient_read_by_handle(uint8 *data);

/**write data to attribute**/
void ble_rsp_attclient_attribute_write(uint8 *data);

/**write data to attribute using ATT write command**/
void ble_rsp_attclient_write_command(uint8 *data);

/**Send confirmation for received indication, use only if manual indications are enabled**/
void ble_rsp_attclient_indicate_confirm(uint8 *data);

/**Read Long Characteristic value**/
void ble_rsp_attclient_read_long(uint8 *data);

/**Send prepare write request to remote host**/
void ble_rsp_attclient_prepare_write(uint8 *data);

/**Send prepare write request to remote host**/
void ble_rsp_attclient_execute_write(uint8 *data);

/**Read multiple attributes from server**/
void ble_rsp_attclient_read_multiple(uint8 *data);

/**Enable encryption on link**/
void ble_rsp_sm_encrypt_start(uint8 *data);

/**Set device to bondable mode**/
void ble_rsp_sm_set_bondable_mode(uint8 *data);

/**delete bonding information from ps store**/
void ble_rsp_sm_delete_bonding(uint8 *data);

/**set pairing requirements**/
void ble_rsp_sm_set_parameters(uint8 *data);

/**Passkey entered**/
void ble_rsp_sm_passkey_entry(uint8 *data);

/**List all bonded devices**/
void ble_rsp_sm_get_bonds(uint8 *data);

/**
		    Set out-of-band encryption data for device
		    Device does not allow any other kind of pairing except oob if oob data is set.
		    **/
void ble_rsp_sm_set_oob_data(uint8 *data);

/**
				Add all bonded devices with a known public or static address to
				the whitelist. Previous entries in the whitelist are first
				cleared. This command can't be used while advertising, scanning
				or being connected.
			**/
void ble_rsp_sm_whitelist_bonds(uint8 *data);

/**Set GAP central/peripheral privacy flags	
			**/
void ble_rsp_gap_set_privacy_flags(uint8 *data);

/**Set discoverable and connectable mode**/
void ble_rsp_gap_set_mode(uint8 *data);

/**start or stop discover procedure**/
void ble_rsp_gap_discover(uint8 *data);

/**Direct connection**/
void ble_rsp_gap_connect_direct(uint8 *data);

/**End current GAP procedure**/
void ble_rsp_gap_end_procedure(uint8 *data);

/**Connect to any device on whitelist**/
void ble_rsp_gap_connect_selective(uint8 *data);

/**Set scan and advertising filtering parameters**/
void ble_rsp_gap_set_filtering(uint8 *data);

/**Set scan parameters**/
void ble_rsp_gap_set_scan_parameters(uint8 *data);

/**Set advertising parameters**/
void ble_rsp_gap_set_adv_parameters(uint8 *data);

/**Set advertisement or scan response data. Use broadcast mode to advertise data**/
void ble_rsp_gap_set_adv_data(uint8 *data);

/**Sets device to Directed Connectable Mode
                        Uses fast advertisement procedure for 1.28s after which device enters nonconnectable mode. 
                        If device has valid reconnection characteristic value, it is used for connection
                        otherwise passed address and address type are used                        
            **/
void ble_rsp_gap_set_directed_connectable_mode(uint8 *data);

/**Set Initiating connection scan parameters**/
void ble_rsp_gap_set_initiating_con_parameters(uint8 *data);

/**Configure I/O-port interrupts**/
void ble_rsp_hardware_io_port_config_irq(uint8 *data);

/**Set soft timer to send events**/
void ble_rsp_hardware_set_soft_timer(uint8 *data);

/**Read A/D conversion**/
void ble_rsp_hardware_adc_read(uint8 *data);

/**Configure I/O-port direction**/
void ble_rsp_hardware_io_port_config_direction(uint8 *data);

/**Configure I/O-port function**/
void ble_rsp_hardware_io_port_config_function(uint8 *data);

/**Configure I/O-port pull-up/pull-down**/
void ble_rsp_hardware_io_port_config_pull(uint8 *data);

/**Write I/O-port**/
void ble_rsp_hardware_io_port_write(uint8 *data);

/**Read I/O-port**/
void ble_rsp_hardware_io_port_read(uint8 *data);

/**Configure SPI**/
void ble_rsp_hardware_spi_config(uint8 *data);

/**Transfer SPI data**/
void ble_rsp_hardware_spi_transfer(uint8 *data);

/**Read data from I2C bus using bit-bang in cc2540. I2C clk is in P1.7 data in P1.6. Pull-up must be enabled on pins**/
void ble_rsp_hardware_i2c_read(uint8 *data);

/**Write data to I2C bus using bit-bang in cc2540. I2C clk is in P1.7 data in P1.6. Pull-up must be enabled on pins**/
void ble_rsp_hardware_i2c_write(uint8 *data);

/**Set TX Power**/
void ble_rsp_hardware_set_txpower(uint8 *data);

/**Set comparator for timer channel**/
void ble_rsp_hardware_timer_comparator(uint8 *data);

/**Enable I/O-port interrupts**/
void ble_rsp_hardware_io_port_irq_enable(uint8 *data);

/**Set I/O-port interrupt direction**/
void ble_rsp_hardware_io_port_irq_direction(uint8 *data);

/**Enable or disable analog comparator**/
void ble_rsp_hardware_analog_comparator_enable(uint8 *data);

/**Read analog comparator output**/
void ble_rsp_hardware_analog_comparator_read(uint8 *data);

/**Configure analog comparator interrupt**/
void ble_rsp_hardware_analog_comparator_config_irq(uint8 *data);

/**Set RX Sensitivity**/
void ble_rsp_hardware_set_rxgain(uint8 *data);

/**Enable or disable USB**/
void ble_rsp_hardware_usb_enable(uint8 *data);

/**Start packet transmission, send one packet at every 625us**/
void ble_rsp_test_phy_tx(uint8 *data);

/**Start receive test**/
void ble_rsp_test_phy_rx(uint8 *data);

/**End test, and report received packets**/
void ble_rsp_test_phy_end(uint8 *data);

/**Reset test**/
void ble_rsp_test_phy_reset(uint8 *data);

/**Get current channel quality map**/
void ble_rsp_test_get_channel_map(uint8 *data);

/**Debug command**/
void ble_rsp_test_debug(uint8 *data);

/**Set channel quality measurement mode**/
void ble_rsp_test_channel_mode(uint8 *data);

/**Reset system**/
void ble_rsp_dfu_reset(uint8 *data);

/**set address for flashing**/
void ble_rsp_dfu_flash_set_address(uint8 *data);

/**Upload binary for flashing. Address will be updated automatically.**/
void ble_rsp_dfu_flash_upload(uint8 *data);

/**Uploading is finished.**/
void ble_rsp_dfu_flash_upload_finish(uint8 *data);

/**Device booted up, and is ready to receive commands**/
void ble_evt_system_boot(uint8 *data);

/**Debug data output**/
void ble_evt_system_debug(uint8 *data);

/**Endpoint receive size crossed watermark**/
void ble_evt_system_endpoint_watermark_rx(uint8 *data);

/**Endpoint transmit size crossed watermark**/
void ble_evt_system_endpoint_watermark_tx(uint8 *data);

/**Script failure detected**/
void ble_evt_system_script_failure(uint8 *data);

/**No valid license key found**/
void ble_evt_system_no_license_key(uint8 *data);

/**Protocol error in command parser**/
void ble_evt_system_protocol_error(uint8 *data);

/**Dump key result**/
void ble_evt_flash_ps_key(uint8 *data);

/**Attribute value has changed**/
void ble_evt_attributes_value(uint8 *data);

/**User-backed attribute data requested**/
void ble_evt_attributes_user_read_request(uint8 *data);

/**attribute status flags has changed**/
void ble_evt_attributes_status(uint8 *data);

/**Connection opened**/
void ble_evt_connection_status(uint8 *data);

/**Remote version information**/
void ble_evt_connection_version_ind(uint8 *data);

/**Remote feature information**/
void ble_evt_connection_feature_ind(uint8 *data);

/**Raw RX**/
void ble_evt_connection_raw_rx(uint8 *data);

/**Link Disconnected**/
void ble_evt_connection_disconnected(uint8 *data);

/**Attribute indication has been acknowledged**/
void ble_evt_attclient_indicated(uint8 *data);

/**GATT procedure completed**/
void ble_evt_attclient_procedure_completed(uint8 *data);

/**group discovery return**/
void ble_evt_attclient_group_found(uint8 *data);

/**characteristics found**/
void ble_evt_attclient_attribute_found(uint8 *data);

/**Handle type mapping found**/
void ble_evt_attclient_find_information_found(uint8 *data);

/**attribute value returned**/
void ble_evt_attclient_attribute_value(uint8 *data);

/**Response to read multiple request**/
void ble_evt_attclient_read_multiple_response(uint8 *data);

/**SMP data output**/
void ble_evt_sm_smp_data(uint8 *data);

/**Link bonding has failed**/
void ble_evt_sm_bonding_fail(uint8 *data);

/**Passkey to be entered to remote device**/
void ble_evt_sm_passkey_display(uint8 *data);

/**Passkey Requested**/
void ble_evt_sm_passkey_request(uint8 *data);

/**Bond status information**/
void ble_evt_sm_bond_status(uint8 *data);

/**Scan Response**/
void ble_evt_gap_scan_response(uint8 *data);

/**Not used**/
void ble_evt_gap_mode_changed(uint8 *data);

/**I/O-port state**/
void ble_evt_hardware_io_port_status(uint8 *data);

/**soft timer event**/
void ble_evt_hardware_soft_timer(uint8 *data);

/**adc result**/
void ble_evt_hardware_adc_result(uint8 *data);

/**This event is produced when analog comparator output changes**/
void ble_evt_hardware_analog_comparator_status(uint8 *data);

/**Device booted up in dfu, and is ready to receive commands**/
void ble_evt_dfu_boot(uint8 *data);

/**Reset device**/
void ble_rsp_system_reset(uint8 *data);

void ble_default(uint8 *data);

#endif
