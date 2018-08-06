# MyAorusKeyboardSDK
Aorus RGB Keyboard SDK for C#

## Introduction ##
The Aorus Fusion application provided with the Aorus series allows to set custom profiles, macros and select among default styles.
While that could be enough for certain users, pro-users and developers might be interested in having more control over their keyboard.

After contacting both Gigabyte and Aorus technical support and being told that a public SDK is not available, I went ahead and made one.

Pull requests and issues are welcome as this is a still very young repository.

## Features ##
- Showing Current Battery Percentage
- Movie Mode when process is fullscreen (turn off the LEDs)
- Support for Random Color Layouts
- Support for OSB 
    - Automatic recording when switching to specific scene

## Requirements: ##
- **Visual Studio 2017**
- **Windows Driver Kit:** https://developer.microsoft.com/windows/hardware/windows-driver-kit

## Tested on ##
- Aorus x5 v7 on Windows 10

---------------

## Keyboard Protocol:

### RGB Keys Data Holder
Each key is defined in `PictureMatrix[960]`

4 bytes (1 byte each field):
1. Key Index [See Helpers.cs] (in bytes)
2. Red Color value [0-255]
3. Green Color value [0-255]
4. Blue Color value [0-255]

-----------------------------------------------

### Initalization:

GUID: `"4d1e55b2-f16f-11cf-88cb-001111000030"`

1. Search for feature with: `"04d9", "8008", "1044", "7a38", "1044" and "7a39"`

2. Get File Handle/IntPtr to the keyboard.

- On Windows: `Win32.CreateFile(stringAuto, 3221225472U, 3, IntPtr.Zero, 3, 0, 0);`

-----------------------------------------------

### Set Keys Matrix

- BufferArray[70]

	- BufferArray[0] = 0
	- BufferArray[1] = 18
	- BufferArray[2] = 0
	- BufferArray[3] = <index_matrix>
	- BufferArray[4] = 8
	- BufferArray[5] = 0
	- BufferArray[6] = 0
	- BufferArray[7] = 0
	- BufferArray[8] = 255 - (188 + (<index_matrix>) + 8)

1. **SetFeature first 9 bytes**
1. *Sleep 65ms*
1. Reset all 70 bytes
1. BufferArray[1-64] = PictureMatrix[0-63]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[64-127]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[128-191]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[192-255]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[256-319]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[320-383]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[384-447]
1. **Write Feature**
1. *Sleep 100ms*
1. BufferArray[1-64] = PictureMatrix[448-511]
1. **Write Feature**
1. *Sleep 100ms*

**Note:** In the original library, the sleep seem to be crucial. 

-----------------------------------------------

### Select Keyboard Light Layout

`<layout_index> - [0-4]`

`<brightness_level> - [0-255]`

`<last_value> = 255 - (8 + (51 + <layout_index>) + 5 + <brightness_level> + 2 + 1)`

Byte array[9]: `[0, 8, 0, 51 + <layout_index>, 5, <brightness_level>, 2, 1, <last_value>]`

-----------------------------------------------

### Select All Green Effect

`<last_value> = 255 - (8 + (51 + <layout_index>) + 5 + <brightness_level> + 2 + 1)`

Byte array[9]: `[0, 8, 0, 1, 5, 50, 2, 1, <last_value>]`

-----------------------------------------------

### Select Breathing Effect

`<last_value> = 255 - (8 + (51 + <layout_index>) + 5 + <brightness_level> + 2 + 1)`

Byte array[9]: `[0, 8, 0, 2, 5, 50, 2, 1, <last_value>]`

-----------------------------------------------

### Select Wave Effect

`<last_value> = 255 - (8 + (51 + <layout_index>) + 5 + <brightness_level> + 2 + 1)`

Byte array[9]: `[0, 8, 0, 3, 5, 50, 8, 1, <last_value>]`

-----------------------------------------------

### Get Current Brightness

Read HID Feature and return `buffer[5]`

-----------------------------------------------