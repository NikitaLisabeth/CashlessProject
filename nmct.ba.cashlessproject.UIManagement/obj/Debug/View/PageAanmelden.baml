   M S B A M L   `   `   `  ����M  IWPF Template NMCT1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullo<clr-namespace:nmct.ba.cashlessproject.UIManagement.ViewModel.nmct.ba.cashlessproject.UIManagement.ViewModel  5      =    7nmct.ba.cashlessproject.UIManagement.View.PageAanmelden   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Kvm<clr-namespace:nmct.ba.cashlessproject.UIManagement.ViewModel     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5	      D  >nmct.ba.cashlessproject.UIManagement.ViewModel.PageAanmeldenVM  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      �� 5      $��3* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $
��227* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $	��70* �6   �� 5      $
��226* �6   .+�5   
   ��    RectangleStyle#��[  6   $��1��6<   $��1��6L   $��5��6Y   $��2��6j   $��	0,0,0,0.2q�6~   �� 5   
   $��2��6   $��1��6!   $��Naam: ��61     TextBlockStyle#��[ 6?   $��10,23.4,343,23.2q�6g   $��2��6�   $��19��6�   �� 5   
   $��3��6   $��1��6!   $��Wachtwoord: ��61   #��[ 6E   $��10,22.8,343,22.8q�6m   $��2��6�   $��20��6�   �� 5   
   $��2��6,   $��2��69     TextBoxStyle#��[ 6I   $��10q�6o   ��6   �� *
Username+6o   �� 5    
   $��3��6,   $��2��69   #��[ 6I   $��10q�6o   ��6   �� *
Password+6o   �� 5!   
   $��4��6   $��1��6!   $��2��61   
���  ��$��0,0.2,0,65.2q�6m   $��2��6�   ��6V   �� *Error+6�   �� 5"   
   $��5��62   $��2��6?     ButtonStyle#��[ 6O   $��	Aanmelden��6t   $��3.6,12.4,222,13.2q�6�   $
��Auto��6�   ��6   �� *LoginCommand+6�   5#      5$      