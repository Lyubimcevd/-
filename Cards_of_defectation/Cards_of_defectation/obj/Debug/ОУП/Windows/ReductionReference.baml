   M S B A M L   `   `   `  ����O  KCards_of_defectation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Cards_of_defectation.ОУП.Windows#Cards_of_defectation.ОУП.Windows  5   	   <    6Cards_of_defectation.ОУП.Windows.ReductionReference   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Clocal1clr-namespace:Cards_of_defectation.ОУП.Windows     5   	   -   5          � Title$  Справочники��5   	   $	��300��6   $	��514��6*    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$
 Save��5
      5      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��3 �6   �� 5      .+�5   
   o� $��0��6   $��3��5      .@�5      m� $��Сохранить��6    m� Command$
 Save��5      5      �� 5   
   $��1��6   $��1��5      $��3��5      $��Stretch=�5       $��Center=�5!      &   System.Windows.Controls.DataGrid  5"   
   -   5          Name$ dataGrid��5"         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5#      $��2��5$      $��1��5%         HeadersVisibility$ Column=�5'      ��5&      �� 	 �� UpdateSourceTrigger$	 PropertyChanged=�5(        5)   
   -   5        $ 	dataGrid1��5)      $ * 5*      $��1��5+      $
 None=�5,      
   
IsReadOnly
 . 5.      5/      50      