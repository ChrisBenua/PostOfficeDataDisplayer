   M S B A M L   `   `   `  ���� S  OPostOfficesDataDisplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PostOfficesDataDisplayerPostOfficesDataDisplayer  ]3clr-namespace:PostOfficesDataDisplayer.UserControls%PostOfficesDataDisplayer.UserControls  )    #PostOfficesDataDisplayer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:PostOfficesDataDisplayer     Lusercontrols3clr-namespace:PostOfficesDataDisplayer.UserControls       � Title$  
MainWindow��$	��450��$	��820��$	��820��.���  � ColumnDefinitions �� $��50 ��� $��90 ��� $��70 ��� $	��160 ��� $	��160 ��� $
��0.6* ��� $��1* � � RowDefinitions �� $��30 ��� $��40 ��� .+�o� $��10��.@�m� $��Save...��$��18(�.@�m� -    m�Name$ mSaveToNewFile��$��Save to new file��m� -   $ mAppendToFile��$��Append to file��m� -   $ mReplaceFile��$��Replace contents of file��m� -   $ mSaveGEOJsonData��$��Save as GEOJson��m� -   $ 	mOpenFile��$
��Open��$��18(�m� $��Sort By��$��18(�.@�m� -   $ mSortByNone��$
��None��m� -   $ mSortByClassOPS��$��ClassOPS��m� -   $ mSortByShortName��$��	ShortName��m� -	   $ mSortByDistToPoint��$��Dist To Coords��m� $��	Filter By��$��18(�.@�m� -
   $ mFilterByNone��$
��None��m� -   $ mFilterByTypeOPS��$��TypeOPS��m� -   $ mFilterByAdmArea��$��AdmArea��m� -   $ 
mOpenOnMap��$	��Map��$��18(�@   :PostOfficesDataDisplayer.UserControls.NumericUpDownElement  -     Name$ mUpDownControl��$��1��   Maximum$	 100��   Minimum$ 0��   InitialValue$ 0��$��40��$��80��$
��Left=�$��33���� -    ��Name$ 
mAddButton��$��1��$��3��   RoundButton#��[  
������$��10, 5q�$��
Add Entity���� -   $ mDeleteButton��$��1��$��4��#��[  
�������$��10, 5q�$��Remove Entity��{�.  &   System.Windows.Controls.DataGrid  -   	  Name$	 dataGrid��$��2��$��10���� -   
 ��Name$
 mHintTextBox��$��1��$��1�� �� AcceptsReturn .  �� MaxLines$ 2�� �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$ Disabled=�" �� VerticalScrollBarVisibility$ Disabled=�
������ .��Total entities in table�� -    ��Name$ mDataSetSizeTextBox��$��2��$��1�� �� TextAlignment$ Center=�$��0�� �� Padding$ 0, 10, 0, 0q��� -   $
 mfilterTextBox��$��5��$��1��
������  .  . �� -   $
 mSortTextBox��$��6��$��1��
������  .  . 