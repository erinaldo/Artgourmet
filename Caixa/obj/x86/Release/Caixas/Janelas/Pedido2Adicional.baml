   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa0    *Artebit.Restaurante.Caixa.Pedido2Adicional   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   @d2http://schemas.microsoft.com/expression/blend/2008  
   -   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�$��Adicional / Guarinção��
�������.��� -    �Name$ 
GridGlobal��$	��390��$	��730�� � RowDefinitions �� $	��325 ��� $	��325 ��� $��70 �.+��� -    ��Name$ borderGuarnicao��$��0��   
BordaClara#��[  $y�Visible=�$��10 10 20 10q�._��  �� $��27 ��� $��50 ��� $	��243 �.+�� $��1��$��5 0 5 0q�.+��� $��Guarnição:��$
��Left=�$��Center=�*  $Telerik.Windows.Controls.RadComboBox  -     Name$ cmbGuarnicao��   
IsEditable . $
��Left=�$	��170�� �� SelectedValuePath$ id�� �� DisplayMemberPath$ codnome��$��Center=�$��5 0 0 0q��� -   	 ��Name$	 btOkGu��$��10 5 0 0q�  BotaoPadrao#��[ $��OK��$��12(�$��30��$��25���� -   
 ��Name$
 txtMsgGu��$�� ��
��� � �$��5 0 0 0q��� -    ��Name$  VerticalScroller_Guarnicao��$y�Visible=�$��2��$��Disabled=�$��Visible=���. $��10 10 10 10q�.���� -    ��Name$ grid_guarnicao������ .R�A�  A� Columns$ 4������ .R��� -	   $��3 0 3 5q�$��85��$��55������ *cor+���� *cor+���� *id+.���� .+��� $��Center=����� *codigo+�� ���� *nome+�� -
   $ borderAdicionais��$��1��#��[  $y�Visible=�$��10 10 20 10q�._��  �� $��27 ��� $��50 ��� $	��243 �.+�� $��1��$��5 0 5 0q�.+��� $��
Adicional:��$
��Left=�$��Center=�  -   $ cmbAdicional�� . $
��Left=�$	��170��$ id��$ codnome��$��Center=�$��5 0 0 0q��� -   $	 btOkAd��$��10 5 0 0q�#��[ $��OK��$��12(�$��30��$��25���� -   $
 txtMsgAd��$�� ��
��� � �$��5 0 0 0q��� -   $  VerticalScroller_Adicional��$y�Visible=�$��2��$��Disabled=�$��Visible=���. $��10 10 10 10q�.���� -   $ grid_adicional������ .R�A� $ 4������ .R��� -   $��3 0 3 5q�$��85��$��55������ *cor+���� *cor+���� *id+.���� .+��� $��Center=����� *codigo+�� ���� *nome+�� $��2��$��Center=�._��� -   $	 Fechar��$��	10 0 60 0q�#��[ $��Fechar��$	��112��$��30��