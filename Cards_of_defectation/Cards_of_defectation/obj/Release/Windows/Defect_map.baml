   M S B A M L   `   `   `  ���� O  KCards_of_defectation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Cards_of_defectation.WindowsCards_of_defectation.Windows  -    'Cards_of_defectation.Windows.Defect_map   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:Cards_of_defectation.Windows     -     � Title$%  Карта дефектации��$	��400�� � 	Resources    Context_menu_row m� Command �� 
TargetType �� Property �� Value��%�   &	        '�� t     7   �� .@�m� $ ApplicationCommands.Cut��m� $ ApplicationCommands.Copy��m� $ ApplicationCommands.Paste���� " ��.���  �@�ContextMenu8   .���  � RowDefinitions �� $��25 ��� $��50 ��� $	��11* �.+�o� -    o�Name$
 menu�����	#FFCECECE.@�m� $��Сохранить��$��25��$��Center=�$��Center=�$
 Save��m� $��Печать карты��$��Center=�$��Center=�$ Print��� -    �Name$	 cap��$��1�� �� $��25 ��� $��25 �	 � ColumnDefinitions	 �� $	��300 ��� $	��250 ��� $	��100 ��� .+��� $
��Wrap=�$��10,0,0,0q����� *	Nom_zak+
 �� StringFormat$
 Заказ № {0}���� $��1��$
��Wrap=�$��10,0,0,0q����� *Cherch+$.
 (Обозначение детали: {0}���� $��1��$
��Wrap=����� *	Ser_nom+$5
 /Серийный номер изделия: {0}���� $��1��$��1��$
��Wrap=����� *
Naim_det+ �� Mode$ OneWay=�$#
 Наименование: {0}���� $��2��$
��Wrap=����� *Nom_sz+$
 Заявка № {0}���� $��2��$��1��$
��Wrap=����� *Kolvo+$
 Кол-во: {0}���� $��3��$
��Wrap=�$��Right=�$��0,0,10,0q����� *Prior+$ OneWay=�$ 
 Приоритет № {0}���� $��3��$��1��$
��Wrap=�$��Right=�$��0,0,10,0q����� *
Nom_kart+$ OneWay=�$
 Карта № {0}��� $��2��$��1��	 �� $	��180 ��� $��5 ��� .+��� $��1��$��Stretch=�$��Stretch=�&   System.Windows.Controls.DataGrid  -     Name$ 
main_table��$��2��   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$) #Обозначение детали��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 3*    MinWidth$	 130Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Cherch_for_screen+   HeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$ Center=�  $ Кол-во��$ 1* $ 50Q� �� *Kolvo+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� "  .���  � ��$ Center=�4  .System.Windows.Controls.DataGridTemplateColumn  $% Описание дефекта��$ 2* $	 120Q� �� "  .���  � ��$ Center=�   CellTemplate �� .R��  �� $��25 ��� $��25 �.+��� -   ���� *Opis_def_list+ �� Text �� *
Opis_def+$ TwoWay=�$ PropertyChanged=��� $��1�� �� TextWrapping$
 Wrap=����� *Opis_def_komment+$ PropertyChanged=�  $1 +Предполагаемая причина��$ 2* $	 160Q� �� "  .���  � ��$ Center=� �� .R��  �� $��25 ��� $��25 �.+��� -   ���� *Prichina_list+ �� *
Prichina+$ TwoWay=�$ PropertyChanged=��� $��1��$
 Wrap=����� *Prichina_komment+$ PropertyChanged=�  $' !Метод определения��$ 2* $	 130Q� �� "  .���  � ��$ Center=� �� .R��  �� $��25 ��� $��25 �.+��� -   ���� *Met_opr_list+ �� *	Met_opr+$ TwoWay=�$ PropertyChanged=��� $��1��$
 Wrap=����� *Met_opr_komment+$ PropertyChanged=�  $1 +Технические требования��$ 2* $	 150Q� �� "  .���  � ��$ Center=� �� .R��  �� $��25 ��� $��25 �.+��� -   ���� *Teh_treb_list+ �� *
Teh_treb+$ TwoWay=�$ PropertyChanged=��� $��1��$
 Wrap=����� *Teh_treb_komment+$ PropertyChanged=�  $' !Способ устранения��$ 2* $	 120Q� �� "  .���  � ��$ Center=� �� .R��� -	   $��25��$��Center=����� *Spos_ustr_list+ �� *	Spos_ustr+$ TwoWay=�$ PropertyChanged=�  $ Цех��$ 1* $ 60Q� �� "  .���  � ��$ Center=� �� .R��� -
   $��25��$��Center=����� *
Ceh_list+ �� *Ceh+$ TwoWay=�$ PropertyChanged=��� -    ��Name$ 
tabControl��.@��� $��Состав��.���� -    ��Name$ listBox_sostav������ .R�� 	 �� �� �� �� .+��� ���� *	Cehs_path+�� $��1������ *Cherch+�� $��2��$��  ���� $��3������ *
Naim_det+
      �� $��Ручной ввод��.���  �� $��30 ��� .+��� -    ��Name$ 
arm_search�� �� 
IsEditable .  �� IsTextSearchEnabled .  �� -   $ listBox_arm��$��2��$��1������ .R��� ���� *Cherch+