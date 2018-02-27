using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property_Management_System
{
    class Analysis
    {
        private int _newPrice;
        private int _month;
        private int _year;
        private List<BookingSingleViewmodel> BookingsList = new List<BookingSingleViewmodel>();

        public int Month { get => _month; set => _month = value; }
        public int Year { get => _year; set => _year = value; }

        public Analysis()
        {
            if (!CheckExistingData())
            {
                //Data does not exist
                GetBookingData();
                var TotalBookedDays = GetTotalBookedDays();
                var StayDurations = GetStayDurations();
                var WaitTime = GetWaitTime();
                //var CurrentPrices = GetCurrentPrices();
                ComparePrevious(TotalBookedDays, StayDurations, WaitTime);
            }
            else
            {
                //Data exists
            }
        }

        private bool CheckExistingData()
        {
            _month = 03;
            _year = 2018;
            var DataExists = false;
            //Check if there is data already for this month.
            var data = Database.DataReader("SELECT ID FROM ANALYSIS WHERE Month = '" + _month + "' AND Year = '" + _year + "'");
            try
            {
                if (data != null)
                {
                    if (data.HasRows)
                    {
                        Log.Commit("[Analysis:CheckExistingData] Data already found for " + _month + ", " + _year + ".");
                        DataExists = true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Commit("[Analysis:GetBookingData] Loading failed: " + e.Message);
            }
            Database.Connection.Close();
            return DataExists;
        }

        private void GetBookingData()
        {
            var data = Database.DataReader("SELECT * FROM BOOKING WHERE MONTH(checkin) = '"+ _month +"' AND YEAR(checkin) = '"+ _year +"'");
            try
            {
                if (data != null)
                {
                    while (data.Read())
                    {
                        BookingsList.Add(new BookingSingleViewmodel
                        {
                            Checkin = (DateTime)data["Checkin"],
                            Checkout = (DateTime)data["Checkout"],
                            BookingDate = (DateTime)data["BookingDate"],
                            Adult = (int)data["Adult"],
                            Children = (int)data["Children"],
                            ID = (int)data["BookingID"]
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Log.Commit("[Analysis:GetBookingData] Loading failed: " + e.Message);
            }
            Database.Connection.Close();
        }

        private int GetTotalBookedDays()
        {
            //Design
            var Goal = 360;

            var TotalBookedDays = 0;
            foreach (var Item in BookingsList)
            {
                var Duration = Item.Checkout - Item.Checkin;
                TotalBookedDays += Duration.Days;
            }
            if(TotalBookedDays < Goal)
            {
                //Decrease the price.
                //If in the past, the price was decreased because of this, 
            }
            else
            {
                //Raise price
                //If in the past the price was increased and total booked days dropped, increase origional by less.
            }
            return TotalBookedDays;
        }

        private Tuple<int, int> GetStayDurations()
        {
            var WeekBooking = 0;
            var NightBooking = 0;
            foreach (var Item in BookingsList)
            {
                var Duration = Item.Checkout - Item.Checkin;
                if (Duration.Days == 7)
                {
                    WeekBooking++;
                }
                else
                {
                    NightBooking++;
                }
            }
            var Durations = new Tuple<int, int>(WeekBooking, NightBooking);
            Log.Commit("[Analysis:GetStayDurations] Durations: " + Durations);
            return Durations;
        }

        private int GetWaitTime()
        {
            List<int> WaitTime = new List<int>();

            foreach (var Item in BookingsList)
            {
                var Duration = Item.Checkin - Item.BookingDate;
                WaitTime.Add(Duration.Days);
            }
            var Average = WaitTime.Sum() / WaitTime.Count;
            Log.Commit("[Analysis:GetWaitTime] Average: " + Average);
            return Average;
        }

        /*private Tuple<int, int> GetCurrentPrices()
        {
            
        }*/

        private void ComparePrevious(int BookedDays, Tuple<int, int> Durations, int WaitTime)
        {
            var Weight = 0;
            var Aspect = "BookedDays";
            var Goal = 360;
            var Prev_BookedDays = 0;
            var Prev_WaitTime = 0;
            var Prev_NightBookings = 0;
            var Prev_WeekBookings = 0;
            var Prev_NightCost = 0;
            var Prev_WeekCost = 0;
            var data = Database.DataReader("SELECT * FROM ANALYSIS WHERE Month = '" + _month + "' AND Year = '" + _year + "'");
            try
            {
                if (data != null)
                {
                    while (data.Read())
                    {
                        Prev_BookedDays = (int)data["BookedDays"];
                        Prev_WaitTime = (int)data["WaitTime"];
                        Prev_NightBookings = (int)data["NightBookings"];
                        Prev_WeekBookings = (int)data["WeekBookings"];
                        Prev_NightCost = (int)data["NightCost"];
                        Prev_WeekCost = (int)data["WeekCost"];
                    }
                }
            }
            catch (Exception e)
            {
                Log.Commit("[Analysis:ComparePrevious] Loading failed: " + e.Message);
            }
            Database.Connection.Close();

            if (BookedDays < Goal)
            {
                if (BookedDays < (0.5 * Goal))
                {
                    Weight = 3;
                }
                else if(BookedDays < (0.75 * Goal))
                {
                    Weight = 2;
                }
                else if (BookedDays < (0.9 * Goal))
                {
                    Weight = 1;
                }
                DecreasePrice(Aspect, Weight);
            }
            else if(BookedDays == Goal)
            {
                Weight = 1;
                IncreasePrice(Aspect, Weight);
            }
            else if (Goal < BookedDays)
            {
                if(Goal < (0.5 * BookedDays))
                {
                    Weight = 3;
                }
                else if (Goal < (0.75 * BookedDays))
                {
                    Weight = 2;
                }
                else if (Goal < (0.9 * BookedDays))
                {
                    Weight = 1;
                }
                IncreasePrice(Aspect, Weight);
            }
        }

        #region Edit Prices
        private void DecreasePrice(string Aspect, int Weight)
        {

        }
        private void IncreasePrice(string Aspect, int Weight)
        {

        }
        #endregion
    }
}
