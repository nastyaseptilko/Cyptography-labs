   M S B A M L   `   `   `  ����@  <Lab04, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab04Lab04  5   	       Lab04.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab04     5   	     � Title$  
MainWindow��5   	   $	��400��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��2* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 
tb_Encrypt��5   /   $��0��6   $��0��6"   $	��150��6C   $	��350��6P    �� TextWrapping$
 Wrap=�6\   " �� VerticalScrollBarVisibility$
 Auto=�6p   �� 5   
   -   5        $��1��5      $��0��6!   $+��%Зашифровать (Цезаря)��6.   $	��150��6M   $��40��6Y   6e   �� 5   
   -   5        $��2��5      $��0��6!   $1��+Зашифровать (Трисемуса)��6.   $	��160��6P   $��40��6\   6h   �� 5   
   -   5        $ 
tb_Decrypt��5   /   $��0��6   $��1��6"   $	��150��6C   $	��350��6P   $
 Wrap=�6\   $
 Auto=�6p   �� 5   
   -   5        $��1��5      $��1��6!   $-��'Расшифровать (Цезаря)��6.   $	��150��6N   $��40��6Z   6f   �� 5   
   -   5        $��2��5      $��1��6!   $3��-Расшифровать (Трисемуса)��6.   $	��160��6Q   $��40��6]   6i   5      5      