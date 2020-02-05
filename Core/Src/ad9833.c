/*
 * ad9833.c
 *
 *  Created on: 5 Feb 2020
 *      Author: Jack Waller
 */
#include "ad9833.h"
#include "main.h"


void AD9833CalculateRegister(long frequency, int waveform){

	float steps_per_Hz = (float)(0x10000000) / refFreq;

	unsigned long int FreqWord = frequency * steps_per_Hz;

	int MSB = ((FreqWord & 0xFFFC000) >> 14);    //Only lower 14 bits are used for data
	int LSB = (FreqWord & 0x3FFF);

	//Set control bits 15 and 14 to 0 and 1, respectively, for frequency register 0
	LSB |= 0x4000;
	MSB |= 0x4000;

	registerValues[0] = 0x2100;
	registerValues[1] = LSB;
	registerValues[2] = MSB;
	registerValues[3] = 0xC000;
	registerValues[4] = waveform; // TODO waveform error check
}

