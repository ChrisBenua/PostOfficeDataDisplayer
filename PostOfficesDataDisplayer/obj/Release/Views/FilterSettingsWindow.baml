   M S B A M L   `   `   `  ���� S  OPostOfficesDataDisplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PostOfficesDataDisplayerPostOfficesDataDisplayer  3    -PostOfficesDataDisplayer.FilterSettingsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:PostOfficesDataDisplayer       � Title$  FilterSettingsWindow��$	��450��$	��800��.���  � ColumnDefinitions �� $��1* ��� $
��2.5* ��� $��1* � � RowDefinitions �� $��1* ��� $��2* ��� $��1* ��� $
��0.3* �.+��� $��1��$��1��   BorderStyle#��[  $	��100��$��40q�._��� -    ��Name$ mFilterStrTextBox��
������ 
������  �� TextAlignment$ Center=�$��25��$��40, 0q�.���� $��1��$��1�� J� IsHitTestVisible .  $��Enter filter string��$��Center=�$��Center=�
�����������  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=�	 �� Triggers	 �� 
 �� Binding
 �� *Text+ �� ElementName$ mFilterStrTextBox�� �� Value  .a���  � y�$ Visible=��� -    ��Name$ mApplyButton��$��1��$��2��  RoundButton#��[ 
�������$��50��$	��200��$
��Next������ " ��	 �� 
 �� *Text+$ mFilterStrTextBox��  .a���  � {� .  