/*
 * ad9833.h
 *
 *  Created on: 5 Feb 2020
 *      Author: Jack Waller
 */

#ifndef INC_AD9833_H_
#define INC_AD9833_H_

#include "main.h"

const float refFreq = 25000000.0;


void AD9833CalculateRegister(long freqency, int waveform);



#endif /* INC_AD9833_H_ */
