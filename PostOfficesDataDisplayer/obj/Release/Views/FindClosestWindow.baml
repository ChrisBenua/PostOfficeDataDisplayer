   M S B A M L   `   `   `  ���� S  OPostOfficesDataDisplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:PostOfficesDataDisplayer.ViewsPostOfficesDataDisplayer.Views  6    0PostOfficesDataDisplayer.Views.FindClosestWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     >local,clr-namespace:PostOfficesDataDisplayer.Views       � Title$  FindClosestWindow��$	��450��$	��800��.���  � ColumnDefinitions �� $
��0.6* ��� �� �� $
��0.6* � � RowDefinitions �� $��0.52* ��� �� $
��0.6* ��� $��0.52* �.+���    BorderStyle#��[  $��1��$��1��$��30, 10q�$	��100��$��2���� -    ��Name$ mxCoordTextBox��$��1��$��1��
������ 
������ $��50��$��50, 0, 30, 0q�$��0, 20, 0 ,0q� �� TextAlignment$ Center=��� -   $ myCoordTextBox��$��2��$��1��
������ 
������ $��50��$��30, 0, 50, 0q�$��0, 20, 0 ,0q�$ Center=��� $��1��$��1��$��Center=�$��60, 0, 77, 0q�$��X_WGS84 coord��
�����������  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=� �� Triggers �� 	 �� Binding	 �� *Text+
 �� ElementName$
 mxCoordTextBox�� �� Value  .a���  � y�$ Visible=��� $��2��$��1��$��Center=�$��60, 0, 77, 0q�$��Y_WGS84 coord��
����������� " ��.���  � y�$ 	Collapsed=� �� 	 �� *Text+$
 myCoordTextBox��  .a���  � y�$ Visible=��� -    ��Name$ mApplyButton��$��2��$��1��$��2��  RoundButton#��[ 
����� �$	��300��$��60��$��Apply��