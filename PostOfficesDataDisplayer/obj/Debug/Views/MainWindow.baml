   M S B A M L   `   `   `  ����S  OPostOfficesDataDisplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PostOfficesDataDisplayerPostOfficesDataDisplayer  5   	   ]3clr-namespace:PostOfficesDataDisplayer.UserControls%PostOfficesDataDisplayer.UserControls  5   	   )    #PostOfficesDataDisplayer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PostOfficesDataDisplayer     5   	   Lusercontrols3clr-namespace:PostOfficesDataDisplayer.UserControls     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��820��6)   $	��820��65   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��50 �6   6,   �� 5      $��90 �6   6,   �� 5      $��70 �6   6,   �� 5      $	��160 �6   6-   �� 5      $	��160 �6   6-   �� 5      $
��0.6* �6   6.   �� 5      $��1* �6   6,    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��40 �6   6*   �� 5      6   .+�5   
   o� $��10��6   .@�5      m� $��Save...��6   $��18(�6(   .@�5      m� -   5         m�Name$ mSaveToNewFile��5      $��Save to new file��63   6O   m� 5       -   5        $ mAppendToFile��5       $��Append to file��62   6L   m� 5!      -   5        $ mReplaceFile��5!      $��Replace contents of file��61   6U   m� 5"      -   5        $ mSaveGEOJsonData��5"      $��Save as GEOJson��65   6P   5$      m� 5%      -   5        $ 	mOpenFile��5%      $
��Open��6*   $��18(�68   6H   m� 5&      $��Sort By��6   $��18(�6(   .@�5'      m� -   5        $ mSortByNone��5'      $
��None��60   6@   m� 5(      -   5        $ mSortByClassOPS��5(      $��ClassOPS��64   6H   m� 5)      -   5        $ mSortByShortName��5)      $��	ShortName��65   6J   m� 5*      -	   5        $ mSortByDistToPoint��5*      $��Dist To Coords��67   6Q   5+      m� 5,      $��	Filter By��6   $��18(�6*   .@�5-      m� -
   5        $ mFilterByNone��5-      $
��None��62   6B   m� 5.      -   5        $ mFilterByTypeOPS��5.      $��TypeOPS��65   6H   m� 5/      -   5        $ mFilterByAdmArea��5/      $��AdmArea��65   6H   50      m� 51      -   5        $ 
mOpenOnMap��51      $	��Map��6+   $��18(�68   6H   52      @   :PostOfficesDataDisplayer.UserControls.NumericUpDownElement  54   
   -   5          Name$ mUpDownControl��54   o   $��1��6,      Maximum$	 100��69      Minimum$ 0��6G      InitialValue$ 0��6S   $��40��6d   $��80��55   ,   $
��Left=�6:   $��33��6U   6f   �� 57   
   -   5         ��Name$ 
mAddButton��57      $��1��6%   $��3��62      RoundButton#��[  6B   
������$��10, 5q�58      $��
Add Entity��6    67   �� 5:   
   -   5        $ mDeleteButton��5:      $��1��6(   $��4��65   #��[  6E   
�������$��10, 5q�5;      $��Remove Entity��6    {�.  6L   &   System.Windows.Controls.DataGrid  5?   
   -   5        	  Name$	 dataGrid��5?      $��2��6%   $��10��62   
 %� VirtualizationMode$
 	Recycling=�6G   ! %� IsVirtualizingWhenGrouping . $��Visible=�5@      $��Visible=�5A      6J   �� 5C   
   -   5         ��Name$ mHintTextBox��5D   9   $��1��5C      $��1��6    �� AcceptsReturn .  �� MaxLines$ 2��6D    �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$ Disabled=�6c   " �� VerticalScrollBarVisibility$ Disabled=�5D      
������ .��5E      Total entities in table6-   �� 5G   
   -   5         ��Name$ mDataSetSizeTextBox��5G      $��2��61   $��1��6A    �� TextAlignment$ Center=�6N   $��0��6e    �� Padding$ 0, 10, 0, 0q�6n   6�   �� 5J   
   -   5        $ mfilterTextBox��5J      $��5��6*   $��1��6:   
������  .  . 5K   )   �� 5M   
   -   5        $ mSortTextBox��5M      $��6��6(   $��1��68   
������  .  . 5N   )   5P      5Q      