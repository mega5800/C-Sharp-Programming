using System;
using System.Collections.Generic;
using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.GarageUtilities;
using Ex03.GarageLogic.VehicleHierarchy;

namespace Ex03.ConsoleUI
{
    public class UIComponent
    {
        private readonly List<object> r_UserEnteredParameters;
        private readonly List<string> r_SupportedVehiclesList;
        private readonly NewVehicleCreator r_NewVehicleCreator;
        private readonly Garage r_GarageInstance;

        public UIComponent()
        {
            this.r_GarageInstance = new Garage();
            this.r_UserEnteredParameters = new List<object>();
            this.r_NewVehicleCreator = new NewVehicleCreator(this.r_GarageInstance);
            this.r_SupportedVehiclesList = this.r_NewVehicleCreator.VehicleBasicInfoList;
        }

        public void RunGarageProgram()
        {
            int optionChosen;
            bool showMainMenu = true;

            while (showMainMenu)
            {
                Console.Clear();
                Console.WriteLine(string.Format("Hello and welcome to our garage{0}", Environment.NewLine));
                Console.WriteLine("1. Enter a vehicle");
                Console.WriteLine("2. Show all vehicle license numbers");
                Console.WriteLine("3. Change vehicle state");
                Console.WriteLine("4. Pump air into all tires of a vehicle");
                Console.WriteLine("5. Fuel a vehicle");
                Console.WriteLine("6. Charge a vehicle");
                Console.WriteLine("7. Show vehicle data");
                Console.WriteLine("8. Exit");
                optionChosen = getValidMenuOption(8, string.Format("{0}Please choose an option number to perform: ", Environment.NewLine));
                switch (optionChosen)
                {
                    case 1:
                        addVehicleToGarage();
                        break;
                    case 2:
                        showLicenseNumbers();
                        break;
                    case 3:
                        changeVehicleRepairState();
                        break;
                    case 4:
                        pumpAirInSelectedVehicleTires();
                        break;
                    case 5:
                        fuelAVehicleByGivenAmount();
                        break;
                    case 6:
                        chargeAVehicleByGivenAmount();
                        break;
                    case 7:
                        showSpecificVehicleInfo();
                        break;
                    case 8:
                        showMessage("Thank you for using our garage", true);
                        Environment.Exit(0);
                        break;
                }

                showMainMenu = doYouWantToContinue();
            }

            showMessage("Thank you for using our garage", true);
        }

        private bool doYouWantToContinue()
        {
            bool answerToReturn = false;

            Console.Write(string.Format("{0}Do you want to continue using the garage system? (y/n): ", Environment.NewLine));
            answerToReturn = getYesOrNoAnswer();

            return answerToReturn;
        }

        private bool getYesOrNoAnswer()
        {
            string answer = string.Empty;
            bool isInputValid = false;

            while (!isInputValid)
            {
                try
                {
                    answer = Console.ReadLine();
                    isInputValid = (answer.Length == 1) && ((answer[0] == 'y') || (answer[0] == 'n'));
                    if (!isInputValid)
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    showMessage(string.Format("Your input was not valid{0}Please enter y/n: ", Environment.NewLine), false);
                }
            }

            return answer[0] == 'y';
        }

        private int getValidMenuOption(int i_AmountOfOptions, string i_StringToPrint)
        {
            int menuOptionChosen = 0;
            string inputChosen = string.Empty;
            bool isInputValid = false;

            while (!isInputValid)
            {
                try
                {
                    Console.Write(i_StringToPrint);
                    menuOptionChosen = int.Parse(Console.ReadLine());
                    checkChosenValue(menuOptionChosen, 1, i_AmountOfOptions, typeof(int));
                    isInputValid = true;
                }
                catch (FormatException)
                {
                    showMessage("You can only insert a number from the shown options", false);
                }
                catch (ValueOutOfRangeException ex)
                {
                    showMessage(ex.Message, false);
                }
            }

            return menuOptionChosen;
        }

        private void checkChosenValue(object i_ObjectToCheck, float i_LowerLimit, float i_UpperLimit, Type i_TypeToConvert)
        {
            if (i_TypeToConvert == typeof(int))
            {
                if (!(((int)i_ObjectToCheck >= i_LowerLimit) && ((int)i_ObjectToCheck <= i_UpperLimit)))
                {
                    throw new ValueOutOfRangeException(i_LowerLimit, i_UpperLimit);
                }
            }

            if (i_TypeToConvert == typeof(float))
            {
                if (!(((float)i_ObjectToCheck >= i_LowerLimit) && ((float)i_ObjectToCheck <= i_UpperLimit)))
                {
                    throw new ValueOutOfRangeException(i_LowerLimit, i_UpperLimit);
                }
            }
        }

        private void addVehicleToGarage()
        {
            bool isNewVehicleCreated;
            int count = 1, menuOptionChosen;
            VehicleOwner vehicleOwner;

            Console.Clear();
            Console.WriteLine(string.Format("We support the following vehicle types:{0}", Environment.NewLine));
            foreach (string supportedVehicle in this.r_SupportedVehiclesList)
            {
                Console.WriteLine(string.Format("{0}. {1}", count, supportedVehicle));
                count++;
            }

            menuOptionChosen = getValidMenuOption(this.r_SupportedVehiclesList.Count, string.Format("{0}Please choose a vehicle to add to the garage: ", Environment.NewLine));
            vehicleOwner = getOwnerDetails();
            getParametersFromUser(this.r_NewVehicleCreator.GetVehicleInputParamList(menuOptionChosen));
            isNewVehicleCreated = this.r_NewVehicleCreator.CreateVehicle(menuOptionChosen, this.r_UserEnteredParameters, vehicleOwner);
            Console.Clear();
            if (isNewVehicleCreated)
            {
                Console.WriteLine("Your vehicle was successfully submitted");
            }
            else
            {
                Console.WriteLine("Your vehicle is already in our garage");
            }
        }

        private void showLicenseNumbers()
        {
            eVehicleRepairStates filter;

            printAllLicenseNumbers();
            Console.Write(string.Format("{0}Choose a filter:", Environment.NewLine));
            filter = (eVehicleRepairStates)getUserChoiceFromEnumValues(typeof(eVehicleRepairStates));
            printLicenseNumbersByFilter(filter);
        }

        private void changeVehicleRepairState()
        {
            int foundVehicleIndex;
            eVehicleRepairStates newState;

            foundVehicleIndex = findVehicleWithSuitableLicenseNumber();
            Console.Write("Choose a new state:");
            newState = (eVehicleRepairStates)getUserChoiceFromEnumValues(typeof(eVehicleRepairStates));
            this.r_GarageInstance.GarageVehiclesList[foundVehicleIndex].VehicleRepairState = newState;
            Console.WriteLine("The update was completed successfully");
        }

        private int findVehicleWithSuitableLicenseNumber()
        {
            bool isLicenseNumberValid = false;
            string licenseNumberToFind = string.Empty;
            int foundVehicleIndex = 0;

            Console.Clear();
            while (!isLicenseNumberValid)
            {
                try
                {
                    Console.Write("Enter a license number to search: ");
                    licenseNumberToFind = Console.ReadLine();
                    foundVehicleIndex = searchLicenseNumber(licenseNumberToFind);
                    isLicenseNumberValid = true;
                }
                catch (ArgumentException ex)
                {
                    showMessage(ex.Message, true);
                }
            }

            Console.Clear();

            return foundVehicleIndex;
        }

        private void showSpecificVehicleInfo()
        {
            int foundVehicleIndex;

            foundVehicleIndex = findVehicleWithSuitableLicenseNumber();
            Console.WriteLine(this.r_GarageInstance.GarageVehiclesList[foundVehicleIndex].ToString());
        }

        private void pumpAirInSelectedVehicleTires()
        {
            bool isValidInput = false;
            int optionChosen = 0;
            int foundVehicleIndex;

            foundVehicleIndex = findVehicleWithSuitableLicenseNumber();
            Console.Clear();
            while (!isValidInput)
            {
                Console.WriteLine(string.Format("Please choose one of following options{0}", Environment.NewLine));
                Console.WriteLine("1. Pump air to maximal air pressure");
                Console.WriteLine("2. Pump air by given amount");
                Console.Write(string.Format("{0}Your choice is: ", Environment.NewLine));
                try
                {
                    optionChosen = int.Parse(Console.ReadLine());
                    if (!(optionChosen == 1 || optionChosen == 2))
                    {
                        throw new ValueOutOfRangeException(1, 2);
                    }

                    isValidInput = true;
                }
                catch (FormatException)
                {
                    showMessage(string.Format("Your input was not valid{0}", Environment.NewLine), true);
                }
                catch (ValueOutOfRangeException ex)
                {
                    showMessage(ex.Message, true);
                }
            }

            switch (optionChosen)
            {
                case 1:
                    this.r_GarageInstance.GarageVehiclesList[foundVehicleIndex].StoredVehicle.PumpAllTires(0, true);
                    Console.WriteLine("Your vehicle's tires are pumped to maximal air pressure");
                    break;
                case 2:
                    getAmountToFill(foundVehicleIndex, "Enter the amount of air you want to pump: ", true);
                    Console.WriteLine("Your vehicle's tires have been pumped successfully");
                    break;
            }
        }

        private void fuelAVehicleByGivenAmount()
        {
            eFuelType fuelType;
            int foundVehicleIndex;

            foundVehicleIndex = findVehicleWithSuitableLicenseNumber();
            if (this.r_GarageInstance.GarageVehiclesList[foundVehicleIndex].StoredVehicle.EnergyType == eEnergyType.FuelPoweredVehicle)
            {
                fuelType = getValidFuelType(foundVehicleIndex);
                getAmountToFill(foundVehicleIndex, "Enter the amount you want to refuel, in liters: ", false);
                Console.WriteLine("Refueling was completed successfully");
            }
            else
            {
                Console.WriteLine("You chose to fuel an electric vehicle, that's not possible");
            }
        }

        private eFuelType getValidFuelType(int i_VehicleIndex)
        {
            bool isFuelTypeValid = false;
            eFuelType fuelType = 0;

            while (!isFuelTypeValid)
            {
                Console.Write("The fuel options are:");
                try
                {
                    fuelType = (eFuelType)getUserChoiceFromEnumValues(typeof(eFuelType));
                    this.r_GarageInstance.GarageVehiclesList[i_VehicleIndex].StoredVehicle.CheckEnergyData(fuelType);
                    isFuelTypeValid = true;
                }
                catch (ArgumentException ex)
                {
                    showMessage(ex.Message, true);
                }
            }

            return fuelType;
        }

        private void getAmountToFill(int i_VehicleIndex, string i_StringToPrint, bool i_IsAirToPump)
        {
            float inputAmount;
            bool isInputAmountValid = false;

            Console.Clear();
            while (!isInputAmountValid)
            {
                Console.Write(i_StringToPrint);
                try
                {
                    inputAmount = float.Parse(Console.ReadLine());
                    if (i_IsAirToPump)
                    {
                        this.r_GarageInstance.GarageVehiclesList[i_VehicleIndex].StoredVehicle.PumpAllTires(inputAmount, false);
                    }
                    else
                    {
                        this.r_GarageInstance.GarageVehiclesList[i_VehicleIndex].StoredVehicle.CheckEnergyData(inputAmount);
                    }

                    isInputAmountValid = true;
                }
                catch (FormatException)
                {
                    showMessage(string.Format("Your input was not valid{0}", Environment.NewLine), true);
                }
                catch (ValueOutOfRangeException ex)
                {
                    showMessage(ex.Message, true);
                }
                catch (ArgumentException ex)
                {
                    showMessage(ex.Message, true);
                }
            }
        }

        private void chargeAVehicleByGivenAmount()
        {
            int foundVehicleIndex;

            foundVehicleIndex = findVehicleWithSuitableLicenseNumber();
            if (this.r_GarageInstance.GarageVehiclesList[foundVehicleIndex].StoredVehicle.EnergyType == eEnergyType.ElectricPoweredVehicle)
            {
                getAmountToFill(foundVehicleIndex, "Enter the amount you want to charge, in minutes: ", false);
                Console.WriteLine("Recharging was completed successfully");
            }
            else
            {
                Console.WriteLine("You chose to charge a fuel vehicle, that's not possible");
            }
        }

        private int searchLicenseNumber(string i_LicenseNumber)
        {
            int foundVehicleIndex = 0;
            bool licenseNumberFound = false;
            List<GarageVehicle> garageVehicleList = this.r_GarageInstance.GarageVehiclesList;

            foreach (GarageVehicle garageVehicle in garageVehicleList)
            {
                if (garageVehicle.StoredVehicle.LicenseNumber == i_LicenseNumber)
                {
                    licenseNumberFound = true;
                    break;
                }

                foundVehicleIndex++;
            }

            if (!licenseNumberFound)
            {
                throw new ArgumentException(string.Format("The license number {0} is not listed in the garage{1}", i_LicenseNumber, Environment.NewLine));
            }

            return foundVehicleIndex;
        }

        private void printAllLicenseNumbers()
        {
            int count = 1;
            List<GarageVehicle> allLicenseNumbersList = this.r_GarageInstance.GarageVehiclesList;

            Console.Clear();
            Console.WriteLine(string.Format("Here's all the license numbers of the vehicles in the garage:{0}", Environment.NewLine));
            foreach (GarageVehicle garageVehicle in allLicenseNumbersList)
            {
                Console.WriteLine(string.Format("{0}. {1}, {2}", count, garageVehicle.StoredVehicle.LicenseNumber, garageVehicle.VehicleRepairState));
                count++;
            }
        }

        private void printLicenseNumbersByFilter(eVehicleRepairStates i_Filter)
        {
            int count = 1;
            List<GarageVehicle> allLicenseNumbersList = this.r_GarageInstance.GarageVehiclesList;

            Console.Clear();
            Console.WriteLine(string.Format("Here's all the license numbers that match the {0} filter:{1}", i_Filter, Environment.NewLine));
            foreach (GarageVehicle garageVehicle in allLicenseNumbersList)
            {
                if (garageVehicle.VehicleRepairState == i_Filter)
                {
                    Console.WriteLine(string.Format("{0}. {1}, {2}", count, garageVehicle.StoredVehicle.LicenseNumber, garageVehicle.VehicleRepairState));
                    count++;
                }
            }

            if (count == 1)
            {
                Console.Clear();
                Console.WriteLine(string.Format("We don't have vehicles that match the filter {0}", i_Filter));
            }
        }

        private VehicleOwner getOwnerDetails()
        {
            string ownerName = string.Empty, ownerPhoneNumber = string.Empty;

            Console.Clear();
            ownerName = getOwnerValidData("Enter your name: ", false);
            Console.Clear();
            ownerPhoneNumber = getOwnerValidData("Enter your phone number: ", true);

            VehicleOwner vehicleOwner = new VehicleOwner(ownerName, ownerPhoneNumber);

            return vehicleOwner;
        }

        private string getOwnerValidData(string i_StringToPrint, bool i_StringNumberCheck)
        {
            bool isInputValid = false;
            string stringToReturn = string.Empty;

            while (!isInputValid)
            {
                try
                {
                    Console.Write(i_StringToPrint);
                    stringToReturn = Console.ReadLine();
                    checkStringValidity(stringToReturn, i_StringNumberCheck);
                    isInputValid = true;
                }
                catch (FormatException)
                {
                    showMessage("Your input was not valid", true);
                }
            }

            return stringToReturn;
        }

        private void checkStringValidity(string i_StringToCheck, bool i_StringNumberCheck)
        {
            for (int i = 0; i < i_StringToCheck.Length; i++)
            {
                if (i_StringNumberCheck)
                {
                    if (!char.IsDigit(i_StringToCheck[i]))
                    {
                        throw new FormatException();
                    }
                }
                else
                {
                    if (!char.IsLetter(i_StringToCheck[i]))
                    {
                        throw new FormatException();
                    }
                }
            }
        }

        private object getParameterInput(VehicleInputParam i_VehicleInput)
        {
            object objectToReturn = null;
            bool isInputValid = false;

            while (!isInputValid)
            {
                try
                {
                    Console.Write(string.Format("Please enter {0}: ", i_VehicleInput.Description));
                    if (i_VehicleInput.ParamType == typeof(bool))
                    {
                        objectToReturn = getYesOrNoAnswer();
                        isInputValid = true;
                    }

                    if (i_VehicleInput.ParamType == typeof(string))
                    {
                        objectToReturn = Console.ReadLine();
                        isInputValid = true;
                    }

                    if (i_VehicleInput.ParamType == typeof(float))
                    {
                        objectToReturn = float.Parse(Console.ReadLine());
                        if (i_VehicleInput.UpperLimit != 0)
                        {
                            checkChosenValue(objectToReturn, 0, i_VehicleInput.UpperLimit, typeof(float));
                        }

                        if ((float)objectToReturn >= 0)
                        {
                            isInputValid = true;
                        }
                        else
                        {
                            showMessage(string.Format("This value must be greater than or equal 0{0}", Environment.NewLine), false);
                        }
                    }

                    if (i_VehicleInput.ParamType == typeof(int))
                    {
                        objectToReturn = int.Parse(Console.ReadLine());
                        if (i_VehicleInput.UpperLimit != 0)
                        {
                            checkChosenValue(objectToReturn, 0, i_VehicleInput.UpperLimit, typeof(int));
                        }

                        if ((int)objectToReturn > 0)
                        {
                            isInputValid = true;
                        }
                        else
                        {
                            showMessage(string.Format("This value must be greater than 0{0}", Environment.NewLine), false);
                        }
                    }

                    if (i_VehicleInput.ParamType.IsEnum)
                    {
                        objectToReturn = getUserChoiceFromEnumValues(i_VehicleInput.ParamType);
                        isInputValid = true;
                    }
                }
                catch (FormatException)
                {
                    showMessage(string.Format("Your input was not valid. Please enter a number{0}", Environment.NewLine), true);
                }
                catch (ValueOutOfRangeException ex)
                {
                    showMessage(string.Format("{0}{1}", ex.Message, Environment.NewLine), true);
                }
            }

            return objectToReturn;
        }

        private void showMessage(string i_StringToPrint, bool i_ClearScreenNeeded)
        {
            if (i_ClearScreenNeeded)
            {
                Console.Clear();
            }

            Console.WriteLine(i_StringToPrint);
        }

        private void getParametersFromUser(List<VehicleInputParam> i_VehicleRequiredParamteres)
        {
            this.r_UserEnteredParameters.Clear();
            foreach (VehicleInputParam currentParam in i_VehicleRequiredParamteres)
            {
                Console.Clear();
                this.r_UserEnteredParameters.Add(getParameterInput(currentParam));
            }
        }

        private object getUserChoiceFromEnumValues(Type i_Enum)
        {
            int currentValueIndex = 1, userSuppliedIndexOfEnumValue;
            object enumValueToReturn = null;

            Console.WriteLine(Environment.NewLine);
            foreach (object enumValue in Enum.GetValues(i_Enum))
            {
                Console.WriteLine(string.Format("{0}. {1}", currentValueIndex, enumValue));
                currentValueIndex++;
            }

            userSuppliedIndexOfEnumValue = getValidMenuOption(Enum.GetNames(i_Enum).Length, string.Format("{0}Please choose one of the options: ", Environment.NewLine));
            enumValueToReturn = Enum.GetValues(i_Enum).GetValue(userSuppliedIndexOfEnumValue - 1);

            return enumValueToReturn;
        }
    }
}