   M S B A M L   `   `   `  ���� O  KCards_of_defectation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Cards_of_defectation.WindowsCards_of_defectation.Windows  0    *Cards_of_defectation.Windows.SlugebZapiska   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:Cards_of_defectation.Windows     -     � Title$  SlugebZapiska�� � 	Resources  �� Color��%D   (��       * SystemColors.HighlightBrushKey+)�� $ 
Aquamarine��.��� -    �Name$ 	main_grid�� � RowDefinitions �� $��25 ��� $
��Auto ��� �� $��75 �.+�o� -    o�Name$
 menu�����	#FFCECECE.@�m� $��Сохранить�� m� Command$
 Save��$��25��m� $��Печать��$ Print��$��25��� -   $	 Cap��$��1�� � ColumnDefinitions �� $	��110 ��� $	��300 ���  �� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 ��� $��25 �.+��� $��Контракт��$��14(�$
��Bold"�$��Center=��� -   $��1�� �� 
IsEditable . 	 �� IsTextSearchEnabled	 .  $��14(�
 �� IsDropDownOpen
 �� *IsDropDownKontract+ �� Text �� *Text_for_filter_kontract+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=����� *Kontract_list+ �� SelectedItem �� *SelectedKontract+$ TwoWay=�$ PropertyChanged=��� $��Номер станции��$��1��$��14(�$
��Bold"�$��Center=��� -   $��1��$��1�� . 	 .  $��14(�
 �� *IsDropDownSer_nom+ �� *Text_for_filter_ser_nom+$ TwoWay=�$ PropertyChanged=����� *Ser_nom_list+ �� *SelectedSer_nom+$ TwoWay=�$ PropertyChanged=��� $��2��$��2��$��14(�$
��Bold"�$��Center=����� *	Izdelie+ �� StringFormat$ Изделие {0}���� $!��Воинская часть��$��3��$��14(�$
��Bold"�$��Center=��� $��3��$��1��$��14(�$
��Bold"����� *
Voin_chast+$ TwoWay=�$ PropertyChanged=��� $��№ заказа��$��4��$��14(�$
��Bold"�$��Center=��� $��4��$��1��$��14(�$
��Bold"����� *	Nom_zak+$ TwoWay=�$ PropertyChanged=��� $��Срок отправки��$��5��$��14(�$
��Bold"�$��Center=�(  "System.Windows.Controls.DatePicker  $��5��$��1��$	��120��$
��Left=�   Text �� *Srok_otprav+$ TwoWay=�$ PropertyChanged=��� $��Примечание: ��$��2��$��14(�$
��Bold"�$��Center=��� $��2��$��5��$��1��$��12(����� *Proizv_chast+$ TwoWay=�$ PropertyChanged=��� $��2��$
��Auto=�.���  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��  �� $��25 ��� .+��� $��Изготовить��$��14(�$
��Bold"�&   System.Windows.Controls.DataGrid  -     Name$ table_izgot��$��1��   AutoGenerateColumns .  $	��Top=�   CanUserAddRows .     CanUserDeleteRows . ���� *Izgot+ J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *
NewExecute+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Чертёж��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150    HeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value$ Center=�   CellTemplate �� .R��� -    . 	 .  
���   �
 �� *IsDropDownCherch+ �� *Text_for_filter_cherch+$ TwoWay=�$ PropertyChanged=����� *Cherch_list+ �� *SelectedCherch+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Наименование��$	 250  �� "  .���  � ��$ Center=� �� .R��� -	    . 	 .  
���   �
 �� *IsDropDownNaim+ �� *Text_for_filter_naim+$ TwoWay=�$ PropertyChanged=����� *	Naim_list+ �� *SelectedNaim+$ TwoWay=�$ PropertyChanged=����� *Color+0  *System.Windows.Controls.DataGridTextColumn  $ Количество��1  +System.Windows.Controls.DataGridBoundColumn    Binding  �� *Kolvo+$ TwoWay=�$ PropertyChanged=�!   ElementStyle! �� " ��.���  � �� �� *
ColorKolvo+  $ Примечание��$ * "   
Foreground
" �   �  �� *Prim+$ TwoWay=�$ PropertyChanged=� �� "  .���  � ��$ Center=�� $��1�� �� $��25 ��� .+��� $$��Отремонтировать��$��14(�$
��Bold"�  -
   $ table_remont��$��1�� .  $	��Top=� .   . ���� *Remont+   $ Чертёж��$	 150  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownCherch+ �� *Text_for_filter_cherch+$ TwoWay=�$ PropertyChanged=����� *Cherch_list+ �� *SelectedCherch+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Наименование��$	 250  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownNaim+ �� *Text_for_filter_naim+$ TwoWay=�$ PropertyChanged=����� *	Naim_list+ �� *SelectedNaim+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Количество��  �� *Kolvo+$ TwoWay=�$ PropertyChanged=�! �� " ��.���  � �� �� *
ColorKolvo+  $ Примечание��$ * 
" �   �  �� *Prim+$ TwoWay=�$ PropertyChanged=� �� "  .���  � ��$ Center=�� $��2�� �� $��25 ��� .+��� $��Приобрести��$��14(�$
��Bold"�  -   $ table_priobr��$��1�� .  $	��Top=� .   . ���� *Priobr+   $ Н/номер��$	 150  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownNNomer+ �� *Text_for_filter_n_nomer+$ TwoWay=�$ PropertyChanged=����� *N_nomer_list+ �� *SelectedN_nomer+$ TwoWay=�$ PropertyChanged=����� *Color+  $  Чертёж,ТУ,ГОСТ��$	 150  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownCherch+ �� *Text_for_filter_cherch+$ TwoWay=�$ PropertyChanged=����� *Cherch_list+ �� *SelectedCherch+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Наименование��$	 250  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownNaim+ �� *Text_for_filter_naim+$ TwoWay=�$ PropertyChanged=����� *	Naim_list+ �� *SelectedNaim+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Количество��  �� *Kolvo+$ TwoWay=�$ PropertyChanged=�! �� " ��.���  � �� �� *
ColorKolvo+  $ Примечание��$ * 
" �   �  �� *Prim+$ TwoWay=�$ PropertyChanged=� �� "  .���  � ��$ Center=�� $��3�� �� $��25 ��� .+��� $%��Сторонний ремонт��$��14(�$
��Bold"�  -   $ table_stor_rem��$��1�� .  $	��Top=� .   . ���� *
Stor_rem+   $ Чертёж��$	 150  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownCherch+ �� *Text_for_filter_cherch+$ TwoWay=�$ PropertyChanged=����� *Cherch_list+ �� *SelectedCherch+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Наименование��$	 250  �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownNaim+ �� *Text_for_filter_naim+$ TwoWay=�$ PropertyChanged=����� *	Naim_list+ �� *SelectedNaim+$ TwoWay=�$ PropertyChanged=����� *Color+  $ Количество��  �� *Kolvo+$ TwoWay=�$ PropertyChanged=�! �� " ��.���  � �� �� *
ColorKolvo+  $) #Завод-изготовитель�� �� "  .���  � ��$ Center=� �� .R��� -    . 	 .  
���   �
 �� *IsDropDownIzgotov+ �� *Text_for_filter_izgotov+$ TwoWay=�$ PropertyChanged=����� *Izgotov_list+ �� *SelectedIzgotov+$ TwoWay=�$ PropertyChanged=�  $ Примечание��$ * 
" �   �  �� *Prim+$ TwoWay=�$ PropertyChanged=� �� "  .���  � ��$ Center=�� $��3�� �� �� $��50 �.+��� $��Примечания��$
��Bold"�$��14(��� $��1��$��12(����� *	Last_prim+$ TwoWay=�$ PropertyChanged=�