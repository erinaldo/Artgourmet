   M S B A M L   `   `   `  ����   AtendimentoPDV6    0Artebit.Restaurante.AtendimentoPDV.Telas.Pessoas   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
  Fd2http://schemas.microsoft.com/expression/blend/2008     	 
  Pmc;http://schemas.openxmlformats.org/markup-compatibility/2006     	 
  )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�$��Quantidade de Pessoas��$	��210��$	��414��
������  � 	Resources    UpButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName  
DownButton��%@  &	        &	     �� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 30����  � �� �� " ��.R�� .+��� $ backImg��$��../Img/setaCimaPedido.png�� P� 	 � {�
 .  . ��� $ backImg�� � ��$$ ../Img/setaCimaPedidoOpaco.png���� " ��.���  � |� .  ��  � �� .  ��  � ��$ 30����  � ��$ 30����  � �� �� " ��.R�� .+��� $ backImg��$ ��../Img/setaBaixoPedido.png�� P� 	 � {�
 .  . ��� $ backImg�� � ��$% ../Img/setaBaixoPedidoOpaco.png��.��� -   $��janela��$	��414��$	��210��$��Center=�$��Center=� � ColumnDefinitions �� $	��150 ���  � RowDefinitions �� $��50 ��� �� .+���  �� CornerRadius$ 5�����#49181A$��1q�$��-2q�$��2��$��3�� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 0Q�   
BlurRadius$ 10Q�._��� $ 5��
������ 
�������$��1q�}�. ����  �� ImageSource$4 ./AtendimentoPDV;component/Img/imagemFundo3.png��� $��0��$��2��$��1��$��2��$��Center=�$	��Top=�$	��100��.+��� -    ��Name$ UpButton��#��[  $	��Top=�$��Center=��� -    ��Name$ txtQtdPessoas��$��Center=�$��Center=�$	��100��$��30��$��1��$��20(� �� TextAlignment$ Center=��� -   $ 
DownButton��#��[ $��Bottom=�$��Center=��� -    ��Name$
 btOK��$��OK��$��30��$��Center=�$��	90 0 0 25q�$��Bottom=�$��0��$��2��$��2�� J� Opacity$	 0.9Q�$	��100���� -   $��
btCancelar��$��Cancelar��$��30��$	 0.9Q�$��Center=�$��
0 0 130 25q�$��0��$��2��$��2��$��Bottom=�$	��100��