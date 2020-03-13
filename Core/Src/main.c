/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * <h2><center>&copy; Copyright (c) 2020 STMicroelectronics.
  * All rights reserved.</center></h2>
  *
  * This software component is licensed by ST under BSD 3-Clause license,
  * the "License"; You may not use this file except in compliance with the
  * License. You may obtain a copy of the License at:
  *                        opensource.org/licenses/BSD-3-Clause
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Includes ------------------------------------------------------------------*/
#include "main.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "string.h"
#include "stdbool.h"
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */

/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
#define ADC_BUF_LEN 10
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
ADC_HandleTypeDef hadc1;
DMA_HandleTypeDef hdma_adc1;

SPI_HandleTypeDef hspi1;
SPI_HandleTypeDef hspi2;

TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim3;

UART_HandleTypeDef huart2;

/* USER CODE BEGIN PV */
uint16_t registerValues[5]; // Buffer to store calculated SPI Tx to transmit to ad9833
bool updateSignalFreqFlag = false; // Flag to request AD9833 frequency update#

uint8_t uartRxBytes[12]; // Buffer to store Recieved UART bytes
uint8_t uartTxBytes[12] = {0xFF, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00};


uint8_t pgaGainValue; // Value to store current PGA gain
bool updatePGAGainFlag = false; // Flag to request PGA gain update

/* Captured Values */
uint32_t CounterOneValue = 0;
uint32_t CounterTwoValue = 0;
uint32_t counterDifference = 0;

/* Capture index */
uint16_t startedCounting = 0;
bool newCaptureValue = false;
bool ledOnFlag = false;
measureFlag = false;

long refSignalFrequency = 0;

float firADC;
uint16_t rawADC;

uint32_t impedanceMag = 0;
uint32_t impedancePhase = 0;

// ADC stuff
ADC_HandleTypeDef hadc1;
DMA_HandleTypeDef hdma_adc1;
uint16_t adc_buf[ADC_BUF_LEN][3];

int waitCount = 0;
uint32_t loopCounter = 0;
uint8_t sysCount = 0;

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_DMA_Init(void);
static void MX_SPI1_Init(void);
static void MX_USART2_UART_Init(void);
static void MX_TIM2_Init(void);
static void MX_ADC1_Init(void);
static void MX_SPI2_Init(void);
static void MX_TIM3_Init(void);
/* USER CODE BEGIN PFP */
void UART_Rx_Handler(void);
void AD9833_Set_Output(void);
void PGA_Set_Gain(void);
bool CalculateRxDataChecksum(void);
uint8_t makeCheckSum(void);
uint32_t measureImpedanceMagnitude(void);
uint32_t measureImpedancePhase(void);
void sendImpedanceMessage(uint32_t mag, uint32_t phase);
void toggleRelay(uint8_t relay);

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */
  

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_DMA_Init();
  MX_SPI1_Init();
  MX_USART2_UART_Init();
  MX_TIM2_Init();
  MX_ADC1_Init();
  MX_SPI2_Init();
  MX_TIM3_Init();
  /* USER CODE BEGIN 2 */

  // Set All SPI Periperal CS High
  HAL_GPIO_WritePin(PGA_CS_GPIO_Port, PGA_CS_Pin, GPIO_PIN_SET);
  HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_SET);

  // Reset AD9833
  HAL_Delay(10);
  uint16_t resetValue = 0x100;
  HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_RESET);
  HAL_SPI_Transmit(&hspi1, (uint8_t*)resetValue, sizeof(resetValue)/sizeof(uint16_t), HAL_MAX_DELAY);
  HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_SET);

  // Reset LEDS
  HAL_GPIO_WritePin(RED_LED_GPIO_Port, RED_LED_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(GRN_LED_GPIO_Port, GRN_LED_Pin, GPIO_PIN_RESET);

  // UART interrupts
  HAL_UART_Receive_IT(&huart2, (uint8_t*)uartRxBytes, 12);
  //HAL_UART_Receive_DMA(&huart2, (uint8_t*)uartRxBytes, 1);

  // Start timer interrupts
  HAL_TIM_IC_Start_IT(&htim2, TIM_CHANNEL_1);
  HAL_TIM_IC_Start_IT(&htim2, TIM_CHANNEL_2);

  // Start ADC DMA thing
  HAL_ADC_Start_DMA(&hadc1, (uint32_t*)adc_buf, 3*ADC_BUF_LEN);

  HAL_TIM_Base_Start_IT(&htim3);

  // Set relays low

  HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_RESET);
  HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_RESET);

  /* USER CODE END 2 */
 
 

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
    /* USER CODE END WHILE */

    /* USER CODE BEGIN 3 */
	  TIM3->CNT = 0;
	  // Start loop timer

	  if (newCaptureValue){
		 newCaptureValue = false;
	  }

	  // Send Data Packets to AD9833
	  if(updateSignalFreqFlag == true){
		  HAL_GPIO_WritePin(RED_LED_GPIO_Port, RED_LED_Pin, GPIO_PIN_SET);
		  AD9833_Set_Output();
		  updateSignalFreqFlag = false;
	  }
	  // Update PGA Gain
	  if(updatePGAGainFlag == true){
		  HAL_GPIO_WritePin(RED_LED_GPIO_Port, RED_LED_Pin, GPIO_PIN_SET);
		  PGA_Set_Gain();
		  updatePGAGainFlag = false;
	  }

	  if(measureFlag == true){
		  HAL_GPIO_WritePin(RED_LED_GPIO_Port, RED_LED_Pin, GPIO_PIN_SET);
		  impedanceMag = measureImpedanceMagnitude();
		  impedancePhase = measureImpedancePhase();
		  sendImpedanceMessage(impedanceMag, impedancePhase);
		  measureFlag = false;
	  }

	  // Toggle LED pin to show we're alive
	  if (loopCounter % 1000 == 0) {
		  HAL_GPIO_TogglePin(GRN_LED_GPIO_Port, GRN_LED_Pin);
		  sysCount++;
	  }

	  if (loopCounter % 2000 == 0) {
		  // Send message
		  float avgADCtemp = 0;
		  for (int i = 0; i < ADC_BUF_LEN; i++) {
			  avgADCtemp += adc_buf[i][2];
		  }
		  float vTemp = ((3.3*(avgADCtemp / ADC_BUF_LEN)/4096 - 0.76) / 25 + 25);
		  asm("NOP");
		  HAL_GPIO_WritePin(RED_LED_GPIO_Port, RED_LED_Pin, GPIO_PIN_SET);
		  uartTxBytes[1] = 0x01;
		  uartTxBytes[3] = sysCount;
		  uint8_t checksum = makeCheckSum();
		  uartTxBytes[11] = checksum;
		  HAL_UART_Transmit_IT(&huart2, (uint8_t *)uartTxBytes, 12);
	  }


	  // Arbitrary Loop Delay To stop spamming everything
	  //HAL_Delay(10);

	  while (TIM3->CNT < 1000) {
		  // Do nothing
		  asm("NOP");
	  }
	  HAL_GPIO_WritePin(RED_LED_GPIO_Port, RED_LED_Pin, GPIO_PIN_RESET);
	  loopCounter++;

  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Configure the main internal regulator output voltage 
  */
  __HAL_RCC_PWR_CLK_ENABLE();
  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);
  /** Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSI;
  RCC_OscInitStruct.PLL.PLLM = 8;
  RCC_OscInitStruct.PLL.PLLN = 180;
  RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
  RCC_OscInitStruct.PLL.PLLQ = 2;
  RCC_OscInitStruct.PLL.PLLR = 2;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  /** Activate the Over-Drive mode 
  */
  if (HAL_PWREx_EnableOverDrive() != HAL_OK)
  {
    Error_Handler();
  }
  /** Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief ADC1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_ADC1_Init(void)
{

  /* USER CODE BEGIN ADC1_Init 0 */

  /* USER CODE END ADC1_Init 0 */

  ADC_ChannelConfTypeDef sConfig = {0};

  /* USER CODE BEGIN ADC1_Init 1 */

  /* USER CODE END ADC1_Init 1 */
  /** Configure the global features of the ADC (Clock, Resolution, Data Alignment and number of conversion) 
  */
  hadc1.Instance = ADC1;
  hadc1.Init.ClockPrescaler = ADC_CLOCK_SYNC_PCLK_DIV4;
  hadc1.Init.Resolution = ADC_RESOLUTION_12B;
  hadc1.Init.ScanConvMode = ENABLE;
  hadc1.Init.ContinuousConvMode = ENABLE;
  hadc1.Init.DiscontinuousConvMode = DISABLE;
  hadc1.Init.ExternalTrigConvEdge = ADC_EXTERNALTRIGCONVEDGE_NONE;
  hadc1.Init.ExternalTrigConv = ADC_SOFTWARE_START;
  hadc1.Init.DataAlign = ADC_DATAALIGN_RIGHT;
  hadc1.Init.NbrOfConversion = 3;
  hadc1.Init.DMAContinuousRequests = ENABLE;
  hadc1.Init.EOCSelection = ADC_EOC_SINGLE_CONV;
  if (HAL_ADC_Init(&hadc1) != HAL_OK)
  {
    Error_Handler();
  }
  /** Configure for the selected ADC regular channel its corresponding rank in the sequencer and its sample time. 
  */
  sConfig.Channel = ADC_CHANNEL_4;
  sConfig.Rank = 1;
  sConfig.SamplingTime = ADC_SAMPLETIME_3CYCLES;
  if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /** Configure for the selected ADC regular channel its corresponding rank in the sequencer and its sample time. 
  */
  sConfig.Channel = ADC_CHANNEL_6;
  sConfig.Rank = 2;
  if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /** Configure for the selected ADC regular channel its corresponding rank in the sequencer and its sample time. 
  */
  sConfig.Channel = ADC_CHANNEL_TEMPSENSOR;
  sConfig.Rank = 3;
  if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN ADC1_Init 2 */

  /* USER CODE END ADC1_Init 2 */

}

/**
  * @brief SPI1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_SPI1_Init(void)
{

  /* USER CODE BEGIN SPI1_Init 0 */

  /* USER CODE END SPI1_Init 0 */

  /* USER CODE BEGIN SPI1_Init 1 */

  /* USER CODE END SPI1_Init 1 */
  /* SPI1 parameter configuration*/
  hspi1.Instance = SPI1;
  hspi1.Init.Mode = SPI_MODE_MASTER;
  hspi1.Init.Direction = SPI_DIRECTION_2LINES;
  hspi1.Init.DataSize = SPI_DATASIZE_16BIT;
  hspi1.Init.CLKPolarity = SPI_POLARITY_HIGH;
  hspi1.Init.CLKPhase = SPI_PHASE_1EDGE;
  hspi1.Init.NSS = SPI_NSS_SOFT;
  hspi1.Init.BaudRatePrescaler = SPI_BAUDRATEPRESCALER_256;
  hspi1.Init.FirstBit = SPI_FIRSTBIT_MSB;
  hspi1.Init.TIMode = SPI_TIMODE_DISABLE;
  hspi1.Init.CRCCalculation = SPI_CRCCALCULATION_DISABLE;
  hspi1.Init.CRCPolynomial = 10;
  if (HAL_SPI_Init(&hspi1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN SPI1_Init 2 */

  /* USER CODE END SPI1_Init 2 */

}

/**
  * @brief SPI2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_SPI2_Init(void)
{

  /* USER CODE BEGIN SPI2_Init 0 */

  /* USER CODE END SPI2_Init 0 */

  /* USER CODE BEGIN SPI2_Init 1 */

  /* USER CODE END SPI2_Init 1 */
  /* SPI2 parameter configuration*/
  hspi2.Instance = SPI2;
  hspi2.Init.Mode = SPI_MODE_MASTER;
  hspi2.Init.Direction = SPI_DIRECTION_2LINES;
  hspi2.Init.DataSize = SPI_DATASIZE_16BIT;
  hspi2.Init.CLKPolarity = SPI_POLARITY_LOW;
  hspi2.Init.CLKPhase = SPI_PHASE_1EDGE;
  hspi2.Init.NSS = SPI_NSS_SOFT;
  hspi2.Init.BaudRatePrescaler = SPI_BAUDRATEPRESCALER_256;
  hspi2.Init.FirstBit = SPI_FIRSTBIT_MSB;
  hspi2.Init.TIMode = SPI_TIMODE_DISABLE;
  hspi2.Init.CRCCalculation = SPI_CRCCALCULATION_DISABLE;
  hspi2.Init.CRCPolynomial = 10;
  if (HAL_SPI_Init(&hspi2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN SPI2_Init 2 */

  /* USER CODE END SPI2_Init 2 */

}

/**
  * @brief TIM2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM2_Init(void)
{

  /* USER CODE BEGIN TIM2_Init 0 */

  /* USER CODE END TIM2_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_IC_InitTypeDef sConfigIC = {0};

  /* USER CODE BEGIN TIM2_Init 1 */

  /* USER CODE END TIM2_Init 1 */
  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 0;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 4294967295;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_IC_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigIC.ICPolarity = TIM_INPUTCHANNELPOLARITY_RISING;
  sConfigIC.ICSelection = TIM_ICSELECTION_DIRECTTI;
  sConfigIC.ICPrescaler = TIM_ICPSC_DIV1;
  sConfigIC.ICFilter = 15;
  if (HAL_TIM_IC_ConfigChannel(&htim2, &sConfigIC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_IC_ConfigChannel(&htim2, &sConfigIC, TIM_CHANNEL_2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM2_Init 2 */


  /* USER CODE END TIM2_Init 2 */

}

/**
  * @brief TIM3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM3_Init(void)
{

  /* USER CODE BEGIN TIM3_Init 0 */

  /* USER CODE END TIM3_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM3_Init 1 */

  /* USER CODE END TIM3_Init 1 */
  htim3.Instance = TIM3;
  htim3.Init.Prescaler = 89;
  htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim3.Init.Period = 65335;
  htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim3.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim3) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim3, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM3_Init 2 */

  /* USER CODE END TIM3_Init 2 */

}

/**
  * @brief USART2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART2_UART_Init(void)
{

  /* USER CODE BEGIN USART2_Init 0 */

  /* USER CODE END USART2_Init 0 */

  /* USER CODE BEGIN USART2_Init 1 */

  /* USER CODE END USART2_Init 1 */
  huart2.Instance = USART2;
  huart2.Init.BaudRate = 115200;
  huart2.Init.WordLength = UART_WORDLENGTH_8B;
  huart2.Init.StopBits = UART_STOPBITS_1;
  huart2.Init.Parity = UART_PARITY_NONE;
  huart2.Init.Mode = UART_MODE_TX_RX;
  huart2.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart2.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart2) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART2_Init 2 */

  /* USER CODE END USART2_Init 2 */

}

/** 
  * Enable DMA controller clock
  */
static void MX_DMA_Init(void) 
{

  /* DMA controller clock enable */
  __HAL_RCC_DMA2_CLK_ENABLE();

  /* DMA interrupt init */
  /* DMA2_Stream0_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(DMA2_Stream0_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(DMA2_Stream0_IRQn);

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOC, FB_SW3_Pin|FB_SW2_Pin|FB_SW1_Pin|PGA_CS_Pin 
                          |FB_SW6_Pin|FB_SW5_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOA, RED_LED_Pin|GRN_LED_Pin|FB_SW4_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pins : FB_SW3_Pin FB_SW2_Pin FB_SW1_Pin PGA_CS_Pin 
                           FB_SW6_Pin FB_SW5_Pin */
  GPIO_InitStruct.Pin = FB_SW3_Pin|FB_SW2_Pin|FB_SW1_Pin|PGA_CS_Pin 
                          |FB_SW6_Pin|FB_SW5_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

  /*Configure GPIO pins : RED_LED_Pin GRN_LED_Pin FB_SW4_Pin */
  GPIO_InitStruct.Pin = RED_LED_Pin|GRN_LED_Pin|FB_SW4_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pin : AD9833_CS_Pin */
  GPIO_InitStruct.Pin = AD9833_CS_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(AD9833_CS_GPIO_Port, &GPIO_InitStruct);

}

/* USER CODE BEGIN 4 */

uint32_t measureImpedanceMagnitude(void) {
	float adcMean = 0;
	for (int i = 0; i < ADC_BUF_LEN; i++) {
	  adcMean += adc_buf[i][0];
	}
	adcMean /= (float)ADC_BUF_LEN;

	return (uint32_t)adcMean;
}

uint32_t measureImpedancePhase(void) {
	int freq = refSignalFrequency;
	double period = 1.0 / (double)freq;

	double periodTicks = (period * 180000000);
	double count = counterDifference;
	uint32_t phase = (uint32_t)(360.0 * count / periodTicks);

	return phase;
}

void sendImpedanceMessage(uint32_t mag, uint32_t phase) {
	uartTxBytes[1] = 0x03;


	uartTxBytes[3] = mag & 0xFF;
	uartTxBytes[4] = (mag & 0xFF00) / 0xFF;
	uartTxBytes[5] = (mag & 0xFF0000) / 0xFFFF;
	uartTxBytes[6] = (mag & 0xFF000000) / 0xFFFFFF;

	uartTxBytes[7] = phase & 0xFF;
	uartTxBytes[8] = (phase & 0xFF00) / 0xFF;
	uartTxBytes[9] = (phase & 0xFF0000) / 0xFFFF;
	uartTxBytes[10] = (phase & 0xFF000000) / 0xFFFFFF;

	uint8_t checksum = makeCheckSum();
	uartTxBytes[11] = checksum;
	HAL_UART_Transmit_IT(&huart2, (uint8_t *)uartTxBytes, 12);
	measureFlag = false;
}

void toggleRelay(uint8_t relay) {
	switch(relay)
		{
		case(0x01):
				HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_SET);
				//HAL_Delay(10);
				HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_RESET);
		  	  	HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_RESET);
		  	  	HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_RESET);
		  	  	HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_RESET);
		  	  	HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_RESET);
				break;
		case(0x02):
				HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_SET);
				//HAL_Delay(10);
				HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_RESET);
				break;
		case(0x03):
				HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_SET);
				//HAL_Delay(10);
				HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_RESET);
				break;
		case(0x04):
				HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_SET);
				//HAL_Delay(10);
				HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_RESET);
				break;
		case(0x05):
				HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_SET);
				//HAL_Delay(10);
				HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_RESET);
				break;
		case(0x06):
				HAL_GPIO_WritePin(FB_SW6_GPIO_Port, FB_SW6_Pin, GPIO_PIN_SET);
				//HAL_Delay(10);
				HAL_GPIO_WritePin(FB_SW5_GPIO_Port, FB_SW5_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW4_GPIO_Port, FB_SW4_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW3_GPIO_Port, FB_SW3_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW2_GPIO_Port, FB_SW2_Pin, GPIO_PIN_RESET);
				HAL_GPIO_WritePin(FB_SW1_GPIO_Port, FB_SW1_Pin, GPIO_PIN_RESET);
				break;
		default:
			break;
		}
}


void HAL_TIM_IC_CaptureCallback(TIM_HandleTypeDef *htim)
{
  if (htim->Instance==TIM2) {
	/*
		This works if you set channel 1 then channel 2 of the timer.
		You can set channel 1 twice but then the difference will be wrong.
		The reading will be 0xFFFFFFFF - CounterOneValue since CounterTwoValue = 0.
	*/
	if(startedCounting == 0) {
      // Get the 1st Input Capture value
      CounterOneValue = HAL_TIM_ReadCapturedValue(htim, TIM_CHANNEL_1);
      startedCounting = 1;
    }
    else {
      // Get the 2nd Input Capture value
      CounterTwoValue = HAL_TIM_ReadCapturedValue(htim, TIM_CHANNEL_2);

      // Timer difference computation
	  if (CounterTwoValue > CounterOneValue) {
		counterDifference = (CounterTwoValue - CounterOneValue);
	  }
	  else { // (CounterTwoValue <= CounterOneValue)
		counterDifference = ((0xFFFFFFFF - CounterOneValue) + CounterTwoValue);
	  }
	  // Set flags
	  startedCounting = 0;
	  newCaptureValue = true;

	  //TIM2->CNT = 0;
    }
  }
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{
	// Handle Recieved Message
	UART_Rx_Handler();

	// Stage Rx of Next UART Message
	HAL_UART_Receive_IT(&huart2, (uint8_t*)uartRxBytes, 12);
}

void UART_Rx_Handler(void)
{

	if(uartRxBytes[0] != 0xFF) 	// If header byte is not first byte abandon
		return;

	//Calculate Checksum to Check Data is Valid
	if(!CalculateRxDataChecksum())
		return;

	// Data is valid so handle it

	int freq = 0;
	int signal = 0;
	// Switch on Data ID
	switch(uartRxBytes[1])
	{
	case(0x00):
			break;
	case(0x01):
			break;
	case(0x02):
			measureFlag = true;
			break;
	case(0x04): // frequency change message
			freq = uartRxBytes[6] | ((int)uartRxBytes[5] << 8) | ((int)uartRxBytes[4] << 16) | ((int)uartRxBytes[3] << 24);
			signal = uartRxBytes[10] | ((int)uartRxBytes[9] << 8) | ((int)uartRxBytes[8] << 16) | ((int)uartRxBytes[7] << 24);
			AD9833CalculateRegister(freq, signal);
			// Set Update Frequency Flag so AD9833 updates in main task loop
			updateSignalFreqFlag = true;
			refSignalFrequency = freq;
			break;
	case(0x05): // set PGA gain message
			pgaGainValue = uartRxBytes[3];
			updatePGAGainFlag = true;
			break;
	case(0x06): // toggle relay
			toggleRelay(uartRxBytes[3]);
			break;
	default:
		break;
	}
}

uint8_t makeCheckSum(void) {
	uint8_t result = 0;

	int i;
	// Checksum is bitwise xor of all bytes except checksum
	for(i = 0; i < (sizeof(uartTxBytes)/sizeof(uint8_t) - 1); i++)
		result = result ^ uartTxBytes[i];
	return result;
}

bool CalculateRxDataChecksum(void)
{
	uint8_t result = 0;

	int i;
	// Checksum is bitwise xor of all bytes except checksum
	for(i = 0; i < (sizeof(uartRxBytes)/sizeof(uint8_t) - 1); i++)
		result = result ^ uartRxBytes[i];

	if(result == uartRxBytes[i])
		return true;
	else
		return false;
}

void AD9833_Set_Output(void)
{
	HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_RESET);
	uint16_t size = 1;
	uint8_t * base = (uint8_t*)registerValues;
	uint8_t * dataPointer = (uint8_t*)registerValues;
	while(dataPointer < base + sizeof(registerValues))
	{
		//HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_RESET);
		HAL_SPI_Transmit(&hspi1, dataPointer, size, HAL_MAX_DELAY);
		//HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_SET);
		dataPointer += sizeof(uint16_t);
	}
	HAL_GPIO_WritePin(AD9833_CS_GPIO_Port, AD9833_CS_Pin, GPIO_PIN_SET);
}

void PGA_Set_Gain(void)
{
	// 8 bit values for seperate instruction and control registers
	// Channel select register is not needed as single channel device used
	uint8_t instructionReg = 0x40; // write to gain register
	uint8_t controlReg;

	// Switch on allowable gain values, and set relevant bits in control reg
	switch(pgaGainValue){
	case 0x0 : // TODO implement shutdown/sleep whatever its called
		break;
	case 1 :
		controlReg = 0x0;
		break;
	case 2 :
		controlReg = 0x1;
		break;
	case 4 :
		controlReg = 0x2;
		break;
	case 5 :
		controlReg = 0x3;
		break;
	case 8:
		controlReg = 0x4;
		break;
	case 10:
		controlReg = 0x5;
		break;
	case 16:
		controlReg = 0x6;
		break;
	case 32:
		controlReg = 0x7;
		break;
	default:	// gain was not valid so don't send a command
		return;
	}

	// Assemble data to send via SPI
	uint16_t spiWord = ((uint16_t)instructionReg << 8) | controlReg;

	// Get Data pointer
	uint8_t * dataAddr = &spiWord;


	// Set CS Low
	HAL_GPIO_WritePin(PGA_CS_GPIO_Port, PGA_CS_Pin, GPIO_PIN_RESET);
	// Send SPI data
	HAL_SPI_Transmit(&hspi2, dataAddr, sizeof(spiWord)/sizeof(uint16_t), HAL_MAX_DELAY);
	// Set CS High
	HAL_GPIO_WritePin(PGA_CS_GPIO_Port, PGA_CS_Pin, GPIO_PIN_SET);


}

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */

  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{ 
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
