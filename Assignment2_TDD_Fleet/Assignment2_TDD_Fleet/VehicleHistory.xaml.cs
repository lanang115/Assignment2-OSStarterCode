﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment2_TDD_Fleet
{
    /// <summary>
    /// Interaction logic for VehicleHistory.xaml
    /// </summary>
    public partial class VehicleHistory : Window
    {
        public CarList carlist;
        public BookingList bookingList;

        public List<Journey> journeys;
        public List<Booking> bookings;
        public List<FuelPurchase> fuelPurchases;
        public List<Service> services;
        public bool servicesListChanged;

        /// <summary>
        /// this is a constructor for this window
        /// </summary>
        public VehicleHistory()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this is a constructor to parse data from vehicle class
        /// </summary>
        /// <param name="registrationID"></param>
        /// <param name="carManufacture"></param>
        /// <param name="carModel"></param>
        /// <param name="carYear"></param>
        /// <param name="fuelType"></param>
        /// <param name="tankCapacity"></param>
        /// <param name="vehicleOdometer"></param>
        /// <param name="serviceCount"></param>
        public VehicleHistory(string registrationID, string carManufacture, string carModel, int carYear, string fuelType, double tankCapacity, int vehicleOdometer, int serviceCount)
        {
            InitializeComponent();
            TextBoxRegistrationIDHistory.Text = registrationID;
            TextBoxManufactureHistory.Text = carManufacture;
            TextBoxCarModelHistory.Text = carModel;
            TextBoxCarYearHistory.Text = carYear.ToString();
            TextBoxFuelTypeHistory.Text = fuelType;
            TextBoxtankCapacityHistory.Text = tankCapacity.ToString();
            TextBoxVehicleOdometerHistory.Text = vehicleOdometer.ToString();
            TextBoxServiceCountHistory.Text = serviceCount.ToString();
        }
        /// <summary>
        /// this is a click event to delete service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForDeleteServices_Click(object sender, RoutedEventArgs e)
        {
            Button deleteServicesButton = (Button)sender;
            Service s = deleteServicesButton.CommandParameter as Service;
            deleteService(s);
            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == s.vehicleID);
            if (relatedVehicle != null)
            {
                List<Service> servicesRelatedWithVehicle = CarList.services.FindAll(service => service.vehicleID == relatedVehicle.Id);
                this.servicesListView.ItemsSource = servicesRelatedWithVehicle;
                servicesListChanged = true;
                relatedVehicle.updateServicesCount(servicesRelatedWithVehicle);
                Service.SaveServices(CarList.services);
                relatedVehicle.SaveVehicles(CarList.vehicles);
            }
            servicesListView.ItemsSource = CarList.services.FindAll(service => service.vehicleID == relatedVehicle.Id);
            servicesListView.Items.Refresh();
        }
        /// <summary>
        /// this is a bool for unit test
        /// to check if the delete button is working or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool deleteService(Service s)
        {
            return CarList.services.Remove(s);
        }

        /// <summary>
        /// this is click event for delete fuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForDeleteFuel_Clicked(object sender, RoutedEventArgs e)
        {
            Button deleteFuelPurchase = (Button)sender;
            FuelPurchase f = deleteFuelPurchase.CommandParameter as FuelPurchase;
            deleteFuelPurchases(f);
            Vehicle relatedVehicle = CarList.vehicles.Find(v => v.Id == f.VId);
            FuelPurchasesViewForHistory.ItemsSource = CarList.fuelPurchases.FindAll(fuel => fuel.VId == relatedVehicle.Id);
            FuelPurchasesViewForHistory.Items.Refresh();
        }
        /// <summary>
        /// this is a bool for unit test
        /// to check if the delete button is working or not
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool deleteFuelPurchases(FuelPurchase f)
        {
            return CarList.fuelPurchases.Remove(f);
        }
    }
}
