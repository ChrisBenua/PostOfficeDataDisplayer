   M S B A M L   `   `   `  ����S  OPostOfficesDataDisplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PostOfficesDataDisplayerPostOfficesDataDisplayer  5   	   3    -PostOfficesDataDisplayer.FilterSettingsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PostOfficesDataDisplayer     5   	     � Title$  FilterSettingsWindow��5   	   $	��450��6&   $	��800��63   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��1* �6   6,   �� 5      $
��2.5* �6   6.   �� 5      $��1* �6   6,    � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $��2* �6   6*   �� 5      $��1* �6   6*   �� 5      $
��0.3* �6   6,   .+�5   
   �� $��1��6   $��1��6!      BorderStyle#��[  6.   $	��100��6S   $��40q�5      ._�5      �� -   5         ��Name$ mFilterStrTextBox��5      
������ 
������  �� TextAlignment$ Center=�5      $��25��6-   $��40, 0q�69   .��6H   5      5       �� 5"   
   $��1��6   $��1��6$    J� IsHitTestVisible .  $��Enter filter string��6J   $��Center=�5#      $��Center=�61   
���������5$      �� 5%       �� 
TargetType" ��6   .�5&      ��  �� Property � y� �� Value$ 	Collapsed=�63   	 �� Triggers	 5'      �� 5(      
 �� Binding
 6&   �� *Text+ �� ElementName$ mFilterStrTextBox�� �� Value  6^   .a�5)      ��  � y�$ Visible=�6;   5*      5,      5.      �� 50   
   -   5         ��Name$ mApplyButton��50      $��1��6'   $��2��67     RoundButton#��[ 6D   
�������$��50��51   (   $	��200��67   $
��Next��6F   ��52      �� 53      " ��6   	 54      �� 55      
 6&   �� *Text+$ mFilterStrTextBox��  6^   .a�56      ��  � {� .  6J   57      59      5;      5=      5>      