﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace AirportBooking
{
    class Reservation
    {
        private string _SeatClass;
        private string _Departing;
        private string _Arriving;
        private string _Time;
        private string _PassengerName;
        private string _PassportNo;

// all setters fire a changeMade event
        public string Departing {
            get { return _Departing; }
            set
            {
                this._Departing = value;
                changeChecker?.valueChanged();
            }
        }

        public string Arriving {
            get { return _Arriving; }
            set
            {
                this._Arriving = value;
                changeChecker?.valueChanged();
            }
        }
        public string Time {
            get { return _Time; }
            set
            {
                this._Time = value;
                changeChecker?.valueChanged();
            }
        }
        
        public string SeatClass {
            get { return _SeatClass; }
            set { 
                this._SeatClass = value;
                changeChecker?.valueChanged();            
            }
        }
        public string PassengerName
        {
            get { return _PassengerName; }
            set
            {
                this._PassengerName = value;
                changeChecker?.valueChanged();
            }
        }
        public string PassportNo
        {
            get { return _PassportNo; }
            set
            {
                this._PassportNo = value;
                changeChecker?.valueChanged();
            }
        }
        public string BookingReference
        {
            get;set;
        }

        public ChangeChecker changeChecker = new ChangeChecker();

        public Reservation()
        {
            void showMessage()
            {
                MessageBox.Show("it's been edited");
            }
           // changeChecker.changeMadeEvent += showMessage;
        }
        public bool isValid()
        {           
            if (
                _Arriving == null ||
                _Departing == null ||
                _PassengerName == null ||
                _SeatClass == null ||
                _Time == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
    public class ChangeChecker
    {
        public delegate void changeMade();
        public event changeMade changeMadeEvent;
        public void valueChanged()
        {
            if (changeMadeEvent != null)
            {
                changeMadeEvent();
            }
        }

    }

}