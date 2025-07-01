Option Strict Off
Option Explicit On 
Imports System.Runtime.InteropServices
Imports System.Text

Module CDIO
    '============================================================-
    ' CDIO.VB
    ' Headder file for CONTEC Digital I/O device
    '                                              CONTEC.Co.,Ltd.
    '============================================================-

    '-------------------------------------------------------------
    'Delegate
    '-------------------------------------------------------------
    Public Delegate Sub PINTCALLBACK(ByVal Id As Short, ByVal wParam As Integer, ByVal lParam As Integer, ByVal Param As IntPtr)
    Public Delegate Sub PTRGCALLBACK(ByVal Id As Short, ByVal wParam As Integer, ByVal lParam As Integer, ByVal Param As IntPtr)

    '-------------------------------------------------------------
    'Function Prototype
    '-------------------------------------------------------------
    'Common Function
    Declare Function DioInit Lib "CDIO.DLL" (ByVal DeviceName As String, ByRef Id As Short) As Integer
    Declare Function DioExit Lib "CDIO.DLL" (ByVal Id As Short) As Integer
    Declare Function DioResetDevice Lib "CDIO.DLL" (ByVal Id As Short) As Integer
    Declare Function DioGetErrorString Lib "CDIO.DLL" (ByVal ErrorCode As Integer, ByVal ErrorString As StringBuilder) As Integer

    ' Digital filter function
    Declare Function DioSetDigitalFilter Lib "CDIO.DLL" (ByVal Id As Short, ByVal FilterValue As Short) As Integer
    Declare Function DioGetDigitalFilter Lib "CDIO.DLL" (ByVal Id As Short, ByRef FilterValue As Short) As Integer

	' I/O Direction function
	Declare Function DioSetIoDirection Lib "CDIO.DLL" (ByVal Id As Short, ByVal dwDir As Integer) As Integer
	Declare Function DioGetIoDirection Lib "CDIO.DLL" (ByVal Id As Short, ByRef dwDir As Integer) As Integer
	Declare Function DioSetIoDirectionEx Lib "CDIO.DLL" (ByVal Id As Short, ByVal dwDir As Integer) As Integer
	Declare Function DioGetIoDirectionEx Lib "CDIO.DLL" (ByVal Id As Short, ByRef dwDir As Integer) As Integer
    Declare Function DioSet8255Mode Lib "CDIO.DLL" (ByVal Id As Short, ByVal ChipNo As Short, ByVal CtrlWord As Short) As Integer
    Declare Function DioGet8255Mode Lib "CDIO.DLL" (ByVal Id As Short, ByVal ChipNo As Short, ByRef CtrlWord As Short) As Integer

    ' Simple I/O function
	Declare Function DioInpByte Lib "CDIO.DLL" (ByVal Id As Short, ByVal PortNo As Short, ByRef Data As Byte) As Integer
	Declare Function DioInpBit Lib "CDIO.DLL" (ByVal Id As Short, ByVal BitNo As Short, ByRef Data As Byte) As Integer
	Declare Function DioOutByte Lib "CDIO.DLL" (ByVal Id As Short, ByVal PortNo As Short, ByVal Data As Byte) As Integer
	Declare Function DioOutBit Lib "CDIO.DLL" (ByVal Id As Short, ByVal BitNo As Short, ByVal Data As Byte) As Integer
	Declare Function DioEchoBackByte Lib "CDIO.DLL" (ByVal Id As Short, ByVal PortNo As Short, ByRef Data As Byte) As Integer
	Declare Function DioEchoBackBit Lib "CDIO.DLL" (ByVal Id As Short, ByVal BitNo As Short, ByRef Data As Byte) As Integer
	
	' Multiple I/O function
	Declare Function DioInpMultiByte Lib "CDIO.DLL" (ByVal Id As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal PortNo() As Short, ByVal PortNum As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal Data() As Byte) As Integer
	Declare Function DioInpMultiBit Lib "CDIO.DLL" (ByVal Id As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal BitNo() As Short, ByVal BitNum As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal Data() As Byte) As Integer
	Declare Function DioOutMultiByte Lib "CDIO.DLL" (ByVal Id As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal PortNo() As Short, ByVal PortNum As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal Data() As Byte) As Integer
	Declare Function DioOutMultiBit Lib "CDIO.DLL" (ByVal Id As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal BitNo() As Short, ByVal BitNum As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal Data() As Byte) As Integer
	Declare Function DioEchoBackMultiByte Lib "CDIO.DLL" (ByVal Id As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal PortNo() As Short, ByVal PortNum As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal Data() As Byte) As Integer
	Declare Function DioEchoBackMultiBit Lib "CDIO.DLL" (ByVal Id As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal BitNo() As Short, ByVal BitNum As Short, <MarshalAs(UnmanagedType.LPArray)> ByVal Data() As Byte) As Integer
	
	' Interrupt function
    Declare Function DioNotifyInterrupt Lib "CDIO.DLL" (ByVal Id As Short, ByVal IntBit As Short, ByVal Logic As Short, ByVal hWnd As Integer) As Integer
    Declare Function DioSetInterruptCallBackProc Lib "CDIO.DLL" (ByVal Id As Short, ByVal pIntCallBack As IntPtr, ByVal Param As IntPtr) As Integer
		
	' Trigger function
	Declare Function DioNotifyTrg Lib "CDIO.DLL" (ByVal Id As Short, ByVal TrgBit As Short, ByVal TrgKind As Short, ByVal Tim As Integer, ByVal hWnd As Integer) As Integer
	Declare Function DioStopNotifyTrg Lib "CDIO.DLL" (ByVal Id As Short, ByVal TrgBit As Short) As Integer
    Declare Function DioSetTrgCallBackProc Lib "CDIO.DLL" (ByVal Id As Short, ByVal pTrgCallBack As IntPtr, ByVal Param As IntPtr) As Integer
	
    ' Information function
    Declare Function DioQueryDeviceName Lib "CDIO.DLL" (ByVal Index As Short, ByVal DeviceName As String, ByVal Device As String) As Integer
    Declare Function DioGetDeviceType Lib "CDIO.DLL" (ByVal Device As String, ByRef DeviceType As Short) As Integer
    Declare Function DioGetMaxPorts Lib "CDIO.DLL" (ByVal Id As Short, ByRef InPortNum As Short, ByRef OutPortNum As Short) As Integer
	Declare Function DioGetDeviceInfo Lib "CDIO.DLL" (ByVal DeviceName As String, ByVal InfoType As Short, ByRef Param1 As Integer, ByRef Param2 As Integer, ByRef Param3 As Integer) As Integer

    ' DM function
    Declare Function DioDmSetDirection Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short) As Integer
    Declare Function DioDmGetDirection Lib "CDIO.DLL" (ByVal Id As Short, ByRef Direction As Short) As Integer
    Declare Function DioDmSetStandAlone Lib "CDIO.DLL" (ByVal Id As Short) As Integer
    Declare Function DioDmSetMaster Lib "CDIO.DLL" (ByVal Id As Short, ByVal ExtSig1 As Short, ByVal ExtSig2 As Short, ByVal ExtSig3 As Short, ByVal MasterHalt As Short, ByVal SlaveHalt As Short) As Integer
    Declare Function DioDmSetSlave Lib "CDIO.DLL" (ByVal Id As Short, ByVal ExtSig1 As Short, ByVal ExtSig2 As Short, ByVal ExtSig3 As Short, ByVal MasterHalt As Short, ByVal SlaveHalt As Short) As Integer
    Declare Function DioDmSetStartTrigger Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal Start As Short) As Integer
    Declare Function DioDmSetStartPattern Lib "CDIO.DLL" (ByVal Id As Short, ByVal Pattern As Integer, ByVal Mask As Integer) As Integer
    Declare Function DioDmSetClockTrigger Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal Clock As Short) As Integer
    Declare Function DioDmSetInternalClock Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal Clock As Integer, ByVal Unit As Short) As Integer
    Declare Function DioDmSetStopTrigger Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal StopTrigger As Short) As Integer
    Declare Function DioDmSetStopNumber Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal StopNumber As Integer) As Integer
    Declare Function DioDmFifoReset Lib "CDIO.DLL" (ByVal Id As Short, ByVal Reset As Short) As Integer
    Declare Function DioDmSetBuffer Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal Buffer As IntPtr, ByVal Length As Integer, ByVal IsRing As Short) As Integer
    Declare Function DioDmSetTransferStartWait Lib "CDIO.DLL" (ByVal Id As Short, ByVal Time As Short) As Integer
    Declare Function DioDmTransferStart Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short) As Integer
    Declare Function DioDmTransferStop Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short) As Integer
    Declare Function DioDmGetStatus Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByRef Status As Integer, ByRef Err As Integer) As Integer
    Declare Function DioDmGetCount Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByRef Count As Integer, ByRef Carry As Integer) As Integer
    Declare Function DioDmGetWritePointer Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByRef WritePointer As Integer, ByRef Count As Integer, ByRef Carry As Integer) As Integer
    Declare Function DioDmSetStopEvent Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal hWnd As Integer) As Integer
    Declare Function DioDmSetCountEvent Lib "CDIO.DLL" (ByVal Id As Short, ByVal Direction As Short, ByVal Count As Integer, ByVal hWnd As Integer) As Integer
   
    ' Demo Device I/O function
	Declare Function DioSetDemoByte Lib "CDIO.DLL" (ByVal Id As Short, ByVal PortNo As Short, ByVal Data As Byte) As Integer
	Declare Function DioSetDemoBit Lib "CDIO.DLL" (ByVal Id As Short, ByVal BitNo As Short, ByVal Data As Byte) As Integer

	'-------------------------------------------------
	' Type definition
	'-------------------------------------------------
	
    Public Const DEVICE_TYPE_ISA As Short = 0               'ISA or C bus
    Public Const DEVICE_TYPE_PCI As Short = 1               'PCI bus
    Public Const DEVICE_TYPE_PCMCI As Short = 2             'PCMCIA
    Public Const DEVICE_TYPE_USB As Short = 3               'USB
    Public Const DEVICE_TYPE_FIT As Short = 4               'FIT

    '-------------------------------------------------
    ' Parameters
    '-------------------------------------------------
    ' I/O
    Public Const DIO_MAX_ACCS_PORTS As Short = 256
	' DioNotifyInt:Logic
	Public Const DIO_INT_NONE As Short = 0
	Public Const DIO_INT_RISE As Short = 1
	Public Const DIO_INT_FALL As Short = 2
	'DioNotifyTrg:TrgKind
	Public Const DIO_TRG_RISE As Short = 1
	Public Const DIO_TRG_FALL As Short = 2
	' Message
	Public Const DIOM_INTERRUPT As Short = &H1300s
    Public Const DIOM_TRIGGER As Short = &H1340S
    Public Const DIO_DMM_STOP As Short = &H1350S
    Public Const DIO_DMM_COUNT As Short = &H1360S
	' Device Information
    Public Const IDIO_DEVICE_TYPE As Short = 0              ' device type.                          Param1:short
    Public Const IDIO_NUMBER_OF_8255 As Short = 1           ' Number of 8255 chip.                  Param1:int
    Public Const IDIO_IS_8255_BOARD As Short = 2            ' Is 8255 board?                        Param1:BOOL(True/False)
    Public Const IDIO_NUMBER_OF_DI_BIT As Short = 3         ' Number of digital input bit.          Param1:int
    Public Const IDIO_NUMBER_OF_DO_BIT As Short = 4         ' Number of digital outout bit.         Param1:int
    Public Const IDIO_NUMBER_OF_DI_PORT As Short = 5        ' Number of digital input port.         Param1:int
    Public Const IDIO_NUMBER_OF_DO_PORT As Short = 6        ' Number of digital output port.        Param1:int
    Public Const IDIO_IS_POSITIVE_LOGIC As Short = 7        ' Is positive logic?                    Param1:BOOL(True/False)
    Public Const IDIO_IS_ECHO_BACK As Short = 8             ' Can echo back output port?            Param1:BOOL(True/False)
    Public Const IDIO_IS_DIRECTION As Short = 9             ' Can DioSetIoDirection function be used?            Param1:BOOL(True/False)
    Public Const IDIO_IS_FILTER As Short = 10               ' Can digital filter be used?           Param1:BOOL(True/False)
    Public Const IDIO_NUMBER_OF_INT_BIT As Short = 11       ' Number of interrupt bit?              Param1:int
    '-------------------------------------------------
    ' Direction
    '-------------------------------------------------
    Public Const PI_32 As Short = 1
    Public Const PO_32 As Short = 2
    Public Const PIO_1616 As Short = 3
    Public Const DIODM_DIR_IN As Short = &H1S
    Public Const DIODM_DIR_OUT As Short = &H2S
    '-------------------------------------------------
    ' Start
    '-------------------------------------------------
    Public Const DIODM_START_SOFT As Short = 1
    Public Const DIODM_START_EXT_RISE As Short = 2
    Public Const DIODM_START_EXT_FALL As Short = 3
    Public Const DIODM_START_PATTERN As Short = 4
    Public Const DIODM_START_EXTSIG_1 As Short = 5
    Public Const DIODM_START_EXTSIG_2 As Short = 6
    Public Const DIODM_START_EXTSIG_3 As Short = 7
    '-------------------------------------------------
    ' Clock
    '-------------------------------------------------
    Public Const DIODM_CLK_CLOCK As Short = 1
    Public Const DIODM_CLK_EXT_TRG As Short = 2
    Public Const DIODM_CLK_HANDSHAKE As Short = 3
    Public Const DIODM_CLK_EXTSIG_1 As Short = 4
    Public Const DIODM_CLK_EXTSIG_2 As Short = 5
    Public Const DIODM_CLK_EXTSIG_3 As Short = 6
    '-------------------------------------------------
    ' Internal Clock
    '-------------------------------------------------
    Public Const DIODM_TIM_UNIT_S As Short = 1
    Public Const DIODM_TIM_UNIT_MS As Short = 2
    Public Const DIODM_TIM_UNIT_US As Short = 3
    Public Const DIODM_TIM_UNIT_NS As Short = 4
    '-------------------------------------------------
    ' Stop
    '-------------------------------------------------
    Public Const DIODM_STOP_SOFT As Short = 1
    Public Const DIODM_STOP_EXT_RISE As Short = 2
    Public Const DIODM_STOP_EXT_FALL As Short = 3
    Public Const DIODM_STOP_NUM As Short = 4
    Public Const DIODM_STOP_EXTSIG_1 As Short = 5
    Public Const DIODM_STOP_EXTSIG_2 As Short = 6
    Public Const DIODM_STOP_EXTSIG_3 As Short = 7
    '-------------------------------------------------
    ' ExtSig
    '-------------------------------------------------
    Public Const DIODM_EXT_START_SOFT_IN As Short = 1
    Public Const DIODM_EXT_STOP_SOFT_IN As Short = 2
    Public Const DIODM_EXT_CLOCK_IN As Short = 3
    Public Const DIODM_EXT_EXT_TRG_IN As Short = 4
    Public Const DIODM_EXT_START_EXT_RISE_IN As Short = 5
    Public Const DIODM_EXT_START_EXT_FALL_IN As Short = 6
    Public Const DIODM_EXT_START_PATTERN_IN As Short = 7
    Public Const DIODM_EXT_STOP_EXT_RISE_IN As Short = 8
    Public Const DIODM_EXT_STOP_EXT_FALL_IN As Short = 9
    Public Const DIODM_EXT_CLOCK_ERROR_IN As Short = 10
    Public Const DIODM_EXT_HANDSHAKE_IN As Short = 11
    Public Const DIODM_EXT_TRNSNUM_IN As Short = 12

    Public Const DIODM_EXT_START_SOFT_OUT As Short = 101
    Public Const DIODM_EXT_STOP_SOFT_OUT As Short = 102
    Public Const DIODM_EXT_CLOCK_OUT As Short = 103
    Public Const DIODM_EXT_EXT_TRG_OUT As Short = 104
    Public Const DIODM_EXT_START_EXT_RISE_OUT As Short = 105
    Public Const DIODM_EXT_START_EXT_FALL_OUT As Short = 106
    Public Const DIODM_EXT_STOP_EXT_RISE_OUT As Short = 107
    Public Const DIODM_EXT_STOP_EXT_FALL_OUT As Short = 108
    Public Const DIODM_EXT_CLOCK_ERROR_OUT As Short = 109
    Public Const DIODM_EXT_HANDSHAKE_OUT As Short = 110
    Public Const DIODM_EXT_TRNSNUM_OUT As Short = 111

    '-------------------------------------------------
    ' Status
    '-------------------------------------------------
    Public Const DIODM_STATUS_BMSTOP As Short = &H1S
    Public Const DIODM_STATUS_PIOSTART As Short = &H2S
    Public Const DIODM_STATUS_PIOSTOP As Short = &H4S
    Public Const DIODM_STATUS_TRGIN As Short = &H8S
    Public Const DIODM_STATUS_OVERRUN As Short = &H10S

    '-------------------------------------------------
    ' Error
    '-------------------------------------------------
    Public Const DIODM_STATUS_FIFOEMPTY As Short = &H1S
    Public Const DIODM_STATUS_FIFOFULL As Short = &H2S
    Public Const DIODM_STATUS_SGOVERIN As Short = &H4S
    Public Const DIODM_STATUS_TRGERR As Short = &H8S
    Public Const DIODM_STATUS_CLKERR As Short = &H10S
    Public Const DIODM_STATUS_SLAVEHALT As Short = &H20S
    Public Const DIODM_STATUS_MASTERHALT As Short = &H40S
    '-------------------------------------------------
    ' Reset
    '-------------------------------------------------
    Public Const DIODM_RESET_FIFO_IN As Short = &H2S
    Public Const DIODM_RESET_FIFO_OUT As Short = &H4S
    '-------------------------------------------------
    ' Buffer Ring
    '-------------------------------------------------
    Public Const DIODM_WRITE_ONCE As Short = 0
    Public Const DIODM_WRITE_RING As Short = 1

    ' Message Number
    Public Const DIODM_COUNT_MESSAGE1 As Short = &H1000S
    Public Const DIODM_STOP_MESSAGE1 As Short = &H1001S
    Public Const DIODM_COUNT_MESSAGE2 As Short = &H1002S
    Public Const DIODM_STOP_MESSAGE2 As Short = &H1003S

    '-------------------------------------------------
    ' Error codes
    '-------------------------------------------------
    ' Initialize Error
    ' Common
    Public Const DIO_ERR_SUCCESS As Short = 0                               ' normal completed
    Public Const DIO_ERR_INI_RESOURCE As Short = 1                          ' invalid resource reference specified
    Public Const DIO_ERR_INI_INTERRUPT As Short = 2                         ' invalid interrupt routine registered
    Public Const DIO_ERR_INI_MEMORY As Short = 3                            ' invalid memory allocationed
    Public Const DIO_ERR_INI_REGISTRY As Short = 4                          ' invalid registry accesse

    Public Const DIO_ERR_SYS_RECOVERED_FROM_STANDBY As Short = 7            ' Execute DioResetDevice function because the device has recovered from standby mode
    Public Const DIO_ERR_INI_NOT_FOUND_SYS_FILE As Short = 8                ' Because the Cdio.sys file is not found, it is not possible to initialize it
    Public Const DIO_ERR_INI_DLL_FILE_VERSION As Short = 9                  ' Because version information on the Cdio.dll file cannot be acquired, it is not possible to initialize it
    Public Const DIO_ERR_INI_SYS_FILE_VERSION As Short = 10                 ' Because version information on the Cdio.sys file cannot be acquired, it is not possible to initialize it
    Public Const DIO_ERR_INI_NO_MATCH_DRV_VERSION As Short = 11             ' Because version information on Cdio.dll and Cdio.sys is different, it is not possible to initialize it

    ' DIO

    ' DLL Error
    ' Common
    Public Const DIO_ERR_DLL_DEVICE_NAME As Short = 10000                   ' invalid device name specified.
    Public Const DIO_ERR_DLL_INVALID_ID As Short = 10001                    ' invalid ID specified.
    Public Const DIO_ERR_DLL_CALL_DRIVER As Short = 10002                   ' not call the driver.(Invalid device I/O controller)
    Public Const DIO_ERR_DLL_CREATE_FILE As Short = 10003                   ' not create the file.(Invalid CreateFile)
    Public Const DIO_ERR_DLL_CLOSE_FILE As Short = 10004                    ' not close the file.(Invalid CloseFile)
    Public Const DIO_ERR_DLL_CREATE_THREAD As Short = 10005                 ' not create the thread.(Invalid CreateThread)
    Public Const DIO_ERR_INFO_INVALID_DEVICE As Short = 10050               ' invalid device infomation specified .Please check the spell.
    Public Const DIO_ERR_INFO_NOT_FIND_DEVICE As Short = 10051              ' not find the available device
    Public Const DIO_ERR_INFO_INVALID_INFOTYPE As Short = 10052             ' specified device infomation type beyond the limit

    ' DIO
    Public Const DIO_ERR_DLL_BUFF_ADDRESS As Short = 10100                  ' invalid data buffer address
    Public Const DIO_ERR_DLL_HWND As Short = 10200                          ' window handle beyond the limit
    Public Const DIO_ERR_DLL_TRG_KIND As Short = 10300                      ' trigger kind beyond the limit

    ' SYS Error
    ' Common
    Public Const DIO_ERR_SYS_MEMORY As Short = 20000                        ' not secure memory
    Public Const DIO_ERR_SYS_NOT_SUPPORTED As Short = 20001                 ' this board couldn't use this function
    Public Const DIO_ERR_SYS_BOARD_EXECUTING As Short = 20002               ' board is behaving, not execute
    Public Const DIO_ERR_SYS_USING_OTHER_PROCESS As Short = 20003           ' other process is using the device, not execute
    Public Const DIO_ERR_SYS_NOT_FOUND_PROCESS_DATA As Short = 20004        ' process information is not found
    
    '#ifndef STATUS_SYS_USB_CRC
    Public Const STATUS_SYS_USB_CRC As Short = 20020                        ' the last data packet received from end point exist CRC error
    Public Const STATUS_SYS_USB_BTSTUFF As Short = 20021                    ' the last data packet received from end point exist bit stuffing offense error
    Public Const STATUS_SYS_USB_DATA_TOGGLE_MISMATCH As Short = 20022       ' the last data packet received from end point exist toggle packet mismatch error
    Public Const STATUS_SYS_USB_STALL_PID As Short = 20023                  ' end point return STALL packet identifier
    Public Const STATUS_SYS_USB_DEV_NOT_RESPONDING As Short = 20024         ' device don't respond to token(IN) ,don't support handshake
    Public Const STATUS_SYS_USB_PID_CHECK_FAILURE As Short = 20025
    Public Const STATUS_SYS_USB_UNEXPECTED_PID As Short = 20026             ' invalid packet identifier received
    Public Const STATUS_SYS_USB_DATA_OVERRUN As Short = 20027               ' end point return data quantity overrun
    Public Const STATUS_SYS_USB_DATA_UNDERRUN As Short = 20028              ' end point return data quantity underrun
    Public Const STATUS_SYS_USB_BUFFER_OVERRUN As Short = 20029             ' IN transmit specified buffer overrun
    Public Const STATUS_SYS_USB_BUFFER_UNDERRUN As Short = 20030            ' OUT transmit specified buffer underrun
    Public Const STATUS_SYS_USB_ENDPOINT_HALTED As Short = 20031            ' end point status is STALL, not transmit
    Public Const STATUS_SYS_USB_NOT_FOUND_DEVINFO As Short = 20032          ' not found device infomation
    Public Const STATUS_SYS_USB_ACCESS_DENIED As Short = 20033              ' Access denied
    Public Const STATUS_SYS_USB_INVALID_HANDLE As Short = 20034             ' Invalid handle
    '#endif
    ' DIO
    Public Const DIO_ERR_SYS_PORT_NO As Short = 20100                       ' board No. beyond the limit
    Public Const DIO_ERR_SYS_PORT_NUM As Short = 20101                      ' board number beyond the limit
    Public Const DIO_ERR_SYS_BIT_NO As Short = 20102                        ' bit No. beyond the limit
    Public Const DIO_ERR_SYS_BIT_NUM As Short = 20103                       ' bit number beyond the limit
    Public Const DIO_ERR_SYS_BIT_DATA As Short = 20104                      ' bit data beyond the limit of 0 to 1
    Public Const DIO_ERR_SYS_INT_BIT As Short = 20200                       ' interrupt bit beyond the limit
    Public Const DIO_ERR_SYS_INT_LOGIC As Short = 20201                     ' interrupt logic beyond the limit
    Public Const DIO_ERR_SYS_TIM As Short = 20300                           ' timer value beyond the limit
    Public Const DIO_ERR_SYS_FILTER As Short = 20400                        ' filter number beyond the limit
    Public Const DIO_ERR_SYS_IODIRECTION As Short = 20500                   ' Direction value is out of range
    Public Const DIO_ERR_SYS_8255 As Short = 20600                          ' 8255 chip number is outside of the range

    ' DM
    Public Const DIO_ERR_SYS_SIGNAL As Short = 21000                        ' Usable signal is outside the setting range
    Public Const DIO_ERR_SYS_START As Short = 21001                         ' Usable start conditions are outside the setting range
    Public Const DIO_ERR_SYS_CLOCK As Short = 21002                         ' Clock conditions are outside the setting range
    Public Const DIO_ERR_SYS_CLOCK_VAL As Short = 21003                     ' Clock value is outside the setting range
    Public Const DIO_ERR_SYS_CLOCK_UNIT As Short = 21004                    ' Clock value unit is outside the setting range
    Public Const DIO_ERR_SYS_STOP As Short = 21005                          ' Stop conditions are outside the setting range
    Public Const DIO_ERR_SYS_STOP_NUM As Short = 21006                      ' Stop number is outside the setting range
    Public Const DIO_ERR_SYS_RESET As Short = 21007                         ' Contents of reset are outside the setting range
    Public Const DIO_ERR_SYS_LEN As Short = 21008                           ' Data number is outside the setting range
    Public Const DIO_ERR_SYS_RING As Short = 21009                          ' Buffer repetition use setup is outside the setting range
    Public Const DIO_ERR_SYS_COUNT As Short = 21010                         ' Data transmission number is outside the setting range
    Public Const DIO_ERR_DM_BUFFER As Short = 21100                         ' Buffer was too large and has not secured
    Public Const DIO_ERR_DM_LOCK_MEMORY As Short = 21101                    ' Memory has not been locked
    Public Const DIO_ERR_DM_PARAM As Short = 21102                          ' Parameter error
    Public Const DIO_ERR_DM_SEQUENCE As Short = 21103                       ' Procedure error of execution
End Module