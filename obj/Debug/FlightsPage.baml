   M S B A M L   `   `   `  ����I  EAirportBooking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AirportBookingAirportBooking  5           AirportBooking.FlightsPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:AirportBooking     5        N� Title$  FlightsPage��5
      .��5      � $��0,0,3,0q�6   $	��750��6   $	��797��6,   $	��623��6;   $	��Top=�6H   ��5   
   �� 5       �� ImageSource$ Airline.jpg��6    � RowDefinitions 5   
   �� 5      $
��322* �6   �� 5      $	��53* �6   .+�5   
   �� -   5         ��Name$ ArrivingBox��5      $
��Left=�6(   $��230,151,0,0q�6C   $	��Top=�6X   $	��120��6p   $��22��6�   5      �� 5   
   -   5        $ DepartingBox��5      $
��Left=�6)   $��
85,151,0,0q�6D   $	��Top=�6X   $	��120��6p    J� RenderTransformOrigin$ 0.011,-0.198&�6|   $��22��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dateControl��5      $
��Left=�6*   $��369,149,0,0q�6E   $	��Top=�6Z   $	��112��6�   $��24��6�   �� 5   
   $��ARRIVING��6   $
��Left=�6#   $��229,116,0,0q�6>   $	��Top=�6S   $��14(�6k   $��29��6y   $��72��6�   �� 5   
   $��	DEPARTING��6   $
��Left=�6$   $��
83,116,0,0q�6?   $	��Top=�6S   $ -0.317,5.385&�6k   $��14(�6�   $��29��6�   $��83��6�   �� 5   
   $
��DATE��6   $
��Left=�6   $��365,116,0,0q�6:   $	��Top=�6O   $��14(�6g   $��29��6u   $��43��6�   �� 5   
   $��LIST VIEW OF FLIGHTS��6   $
��Left=�6/   $��
52,249,0,0q�6J   $	��Top=�6^   $��26��6v   $	��128��6�   �� 5   
   $��HOURS / MINUTES��6   $
��Left=�6*   $��512,117,0,0q�6E   $	��Top=�6Z   
�������$��14(�6�   $��29��6�   $	��127��6�   �� 5   
   -   5         ��Name$ FlightsList��5      $��0,280,255,40q�6(   $��Right=�6>   $	��490��6Z   4  .System.Windows.Automation.AutomationProperties   IsColumnHeader .  �� View 5      �� 5       .P�5!      �� 5"      5$      �� 5&   
   -   5        	 ��Name$	 cancel��5&      $��Cancel��6!   $��577,280,120,314q�6G   $	��100��6`   $��40��6l   ���	#FFE04826���	#FFFF831B6�   �� 5(   
   -   5        $	 update��5(      $��Update��6!   $
��Left=�62   $��577,340,0,0q�6M   $	��Top=�6b   $	��100��6z   $��40��6�   ���	#FFD44040���	#FFFF831B{�.  
���   �6�   �� 5)   
   -   5        $	 delete��5)      $��Delete��6!   $
��Left=�62   $��577,394,0,0q�6M   $	��Top=�6b   $	��100��6z   {�.  $ -0.133,0.909&�6�   $��40��6�   ���	#FFFF831B
���   �6�   �� 5*   
   -   5        $	 create��5*      $��Create��6!   $
��Left=�62   $��577,452,0,0q�6M   $	��Top=�6b   $	��100��6z   {�.  $��40��6�   
���   ����	#FFFF831B
 J� OpacityMask
 �	#FF5352526�   �� 5+   
   -	   5         ��Name$ hoursBox��5+      $
��Left=�6$   $��23��6?   $��512,148,0,0q�6K    �� TextWrapping$
 Wrap=�6`   $��00��6t   $	��Top=�6~   $��55��6�   6�   �� 5,   
   -
   5        $ 
minutesBox��5,      $
��Left=�6&   $��23��6A   $��572,149,0,0q�6M   $
 Wrap=�6b   $��00��6v   $	��Top=�6�   $��70��6�   6�   �� 5-   
   $��CYANAIR - Admin��6   $
��Left=�6*   $��	70,20,0,0q�6E   $	��Top=�6X   ���	#FFFDFDFD$��36(�6�   $��
Levenim MT)�6�   $��ExtraExpanded&�6�   $��	ExtraBold"�6�   $��60��6�   $	��328��6�   �� 5.   
   $��EDIT FLIGHT SCHEDULE��6   $
��Left=�61   $	��122��6L   $��	52,87,0,0q�6Y   $	��Top=�6l   $	��625��6�   
�������$��14(�6�   51      52      