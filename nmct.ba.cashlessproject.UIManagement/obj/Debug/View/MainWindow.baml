   M S B A M L   `   `   `  ����M  IWPF Template NMCT1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:nmct.ba.cashlessproject.UIManagement.View)nmct.ba.cashlessproject.UIManagement.View  5   	   o<clr-namespace:nmct.ba.cashlessproject.UIManagement.ViewModel.nmct.ba.cashlessproject.UIManagement.ViewModel  5   	   5    /nmct.ba.cashlessproject.UIManagement.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Ev7clr-namespace:nmct.ba.cashlessproject.UIManagement.View     5   	   Kvm<clr-namespace:nmct.ba.cashlessproject.UIManagement.ViewModel     5   	     � Title$  
Management��5   	   $	��750��6   $
��1200��6)    � DataContext 5      B  <nmct.ba.cashlessproject.UIManagement.ViewModel.ApplicationVM  5   
    � 	Resources 5
       �� DataTypeD  >nmct.ba.cashlessproject.UIManagement.ViewModel.PageAanmeldenVM=   7nmct.ba.cashlessproject.UIManagement.View.PageAanmelden@  :nmct.ba.cashlessproject.UIManagement.ViewModel.PageKassaVM9   3nmct.ba.cashlessproject.UIManagement.View.PageKassa>  8nmct.ba.cashlessproject.UIManagement.ViewModel.PageOneVM7   1nmct.ba.cashlessproject.UIManagement.View.PageOneB  <nmct.ba.cashlessproject.UIManagement.ViewModel.PageKlantenVM;	   5nmct.ba.cashlessproject.UIManagement.View.PageKlantenF
  @nmct.ba.cashlessproject.UIManagement.ViewModel.PageMedewerkersVM?   9nmct.ba.cashlessproject.UIManagement.View.PageMedewerkersB  <nmct.ba.cashlessproject.UIManagement.ViewModel.PageProductVM;   5nmct.ba.cashlessproject.UIManagement.View.PageProductE  ?nmct.ba.cashlessproject.UIManagement.ViewModel.PageStatistiekVM>   8nmct.ba.cashlessproject.UIManagement.View.PageStatistiek��%  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)(�� �     5      *,
 +)(�� �     5      *, +)(�� ,    5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5      	  5      �� 5   
   " 
 6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5       .��5!      �  � ColumnDefinitions 5"   
   �� 5#      $
��212* �6   �� 5$      $	��47* �6    � RowDefinitions 5'   
   �� 5(      $��50 �6   �� 5)      $��2* �6   .+�5.   
   �� $��0��65   $��0��6E   $��2��6R   $��0,0,0,0q�6f   ��6   �� *Pages+��5/      �� 50      .R�51      ��    HeaderStyle#��[  6   ��6C   �� *Name+��6\   �� *DataContext.ChangePageCommand+ �� RelativeSource ��  �� AncestorType" ���6�   �� 52      ��54      �� 55      .R�56      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   57      59      �� 5;   
   $��1��6   $��2��6F   $��0,0,0,0q�6Z   ��6&   �� *CurrentPage+6Z   5<      5=      