﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_TDD_Fleet
{
    public class Booking
    {
        public Guid id { get; set; }
        public Guid Vehicleid { get; set; }
        public string CustomerName { get; set; }
        public string SelectedVehicle { get; set; }
        public BookingType RentalType { get; set; }
        public int StartOdometer { get; set; }
        public DateTime StartRentDate { get; set; }
        public DateTime EndRentDate { get; set; }
        public double RentPrice { get; set; }

        /// <summary>
        /// this is constructor
        /// </summary>
        public Booking()
        {

        }

        [JsonIgnore]
        public bool bookingListChanged = false;

        /// <summary>
        /// this is a save method for bookings
        /// </summary>
        /// <param name="bookings"></param>
        public void SaveBookings(List<Booking> bookings)
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@companyFileName, JsonConvert.SerializeObject(CompanyList));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText("Bookings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, bookings);
            } 
            bookingListChanged = false;
        }
        /// <summary>
        /// this is a method to update the rent price or rental cost
        /// </summary>
        /// <param name="bookingJourneys"></param>
        public void updateRentPrice(List<Journey> bookingJourneys)
        {
            if (this.RentalType == BookingType.Day)
            {
                if (DateTime.Compare(this.StartRentDate, this.EndRentDate) == 0)
                {
                    this.RentPrice = 100 * 1.0;
                }
                else
                {
                    int daysElapsed = (int)(this.EndRentDate - this.StartRentDate).TotalDays;
                    this.RentPrice = daysElapsed * 100 * 1.0;
                }
            }
            else
            {
                if (bookingJourneys == null || bookingJourneys.Count == 0)
                {
                    this.RentPrice = 0.0;
                }
                else
                {
                    var totalDistance = 0;

                    bookingJourneys.ForEach(j =>
                    {
                        totalDistance += j.EndOdometer - j.StartOdometer;
                    });
                    this.RentPrice = totalDistance * 1.0;
                }

            }
            SaveBookings(CarList.bookings);
        }
    }
}
