   M S B A M L   `   `   `  ����M  IWPF Template NMCT1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:nmct.ba.cashlessproject.UIKlant.Converter)nmct.ba.cashlessproject.UIKlant.Converter  5      e7clr-namespace:nmct.ba.cashlessproject.UIKlant.ViewModel)nmct.ba.cashlessproject.UIKlant.ViewModel  5      7    1nmct.ba.cashlessproject.UIKlant.View.PageGegevens   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Ec7clr-namespace:nmct.ba.cashlessproject.UIKlant.Converter     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Fvm7clr-namespace:nmct.ba.cashlessproject.UIKlant.ViewModel     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5      F   @nmct.ba.cashlessproject.UIKlant.Converter.StringToImageConverter   StringToImageConverter��%   &	          5   
   6#   5      .��5      �  � ColumnDefinitions 5   
   �� 5   
   $��3* �6   �� 5   
   $	��50* �6   �� 5   
   $	��50* �6   �� 5   
   $	��50* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6   .+�5%   
   ��   TextBlockStyle2#��[ 6   $��Uw gegevens:��6=   $��Center=�6Q   $��1��6l   $��0��6|   $��2��6�   �� 5&   
     TextBlockStyle#��[ 6   $��Naam:��6<   $��1��6I   $��2��6Y   $��Center=�6f   �� 5'   
   #��[ 6   $��Adres:��6<   $��1��6J   $��3��6Z   $��Center=�6g   �� 5(   
   #��[ 6   $��Geboortedatum:��6<   $��1��6R   $��4��6b   $��Center=�6o   �� 5)   
   #��[ 6   $��	Geslacht:��6<   $��1��6M   $��5��6]   $��Center=�6j   �� 5*   
   #��[ 6   $��Huidig saldo:��6<   $��1��6Q   $��6��6a   $��Center=�6n   �� 5,   
   -   5        $��txbNaam��5,   g     TextBoxStyle#��[ 6   $��2��6v   $��2��6�   ��68   �� *SelectedCustomer.CustomerName+6�   �� 5-   
   -   5        $��txbAdres��5-   b   #��[ 6   $��2��6r   $��3��6�   $	��0,1q�6�   ��68   �� *SelectedCustomer.Address+6�   �� 5.   
   -   5        $��txbGeboorteDatum��5.   d   #��[ 6   $��2��6|   $��4��6�   $	��0,1q�6�   ��68   �� *SelectedCustomer.BirthDate+6�   �� 5/   
   -   5        $��txbGeslacht��5/   ^   #��[ 6   $��2��6q   $��5��6�   $	��0,1q�6�   ��68   �� *SelectedCustomer.Sex+6�   �� 50   
   -   5        $��txbSaldo��50   b   #��[ 6   $��2��6r   $��6��6�   $	��0,1q�6�   ��68   �� *SelectedCustomer.Balance+6�   �� 52   
   $��4��6<   $��2��6L   $��5��6Y   $��25q�6j   ��6   �� *SelectedCustomer.Picture+6j   �� 55   
     ButtonStyle#��[ 6   $��Kaart opladen��6Y   $��8��6q   $��1��6~   $��2��6�   $��2��6�   $	��5,1q�6�   ��66   �� *OpladenCommand+6�   �� 56   
     ButtonStyle2#��[ 6   $��8��6X   $��3��6e   $��2��6u   $
��50,0q�6�   $��Terug��6�   ��67   �� *TerugCommand+6�   57      58      