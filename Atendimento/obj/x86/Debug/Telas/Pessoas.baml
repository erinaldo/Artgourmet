   M S B A M L   `   `   `  ����  AtendimentoPDV6    0Artebit.Restaurante.AtendimentoPDV.Telas.Pessoas   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
  5      Fd2http://schemas.microsoft.com/expression/blend/2008     	 
  5      Pmc;http://schemas.openxmlformats.org/markup-compatibility/2006     	 
  5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   V   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5      $��Quantidade de Pessoas��6J   $	��210��5   [   $	��414��6h   
������  � 	Resources 5         UpButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName  
DownButton��%�  &	        &	 �    �� 5   
   " ��6!   .�5      ��  � |� .  6*   �� 5       � �� .  6*   �� 5       � ��$ 30��6'   �� 5       � ��$ 30��6&   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ backImg��6$   $��../Img/setaCimaPedido.png��65   5       5      P� 5      	 � {�
 .  . �5   "   �� $ backImg��6)    � ��$$ ../Img/setaCimaPedidoOpaco.png��6P   5      5      5!      5"      �� 5$   
   " ��6#   .�5%      ��  � |� .  6*   �� 5&       � �� .  6*   �� 5'       � ��$ 30��6'   �� 5(       � ��$ 30��6&   �� 5)       � �� 5*      �� 5+      " ��6&   .R�5,      � .+�5-      �� $ backImg��6$   $ ��../Img/setaBaixoPedido.png��65   5.       50      P� 51      	 � {�
 .  . �52   "   �� $ backImg��6)    � ��$% ../Img/setaBaixoPedidoOpaco.png��6P   53      56      58      59      5;      .��5>      � -   5        $��janela��5>      $	��414��6   $	��210��6%   $��Center=�62   $��Center=�6O    � ColumnDefinitions 5A   
   �� 5B      $	��150 �6   �� 5C       � RowDefinitions 5E   
   �� 5F      $��50 �6   �� 5G      �� 5H      .+�5L   
   ��  �� CornerRadius$ 5��6   ���#49181A$��1q�68   $��-2q�6L   $��2��6X   $��3��5M       J� Effect 5N      3  -System.Windows.Media.Effects.DropShadowEffect  5O         ShadowDepth$ 0Q�6#      
BlurRadius$ 10Q�63   ._�5S      �� $ 5��6   
������ 
�������$��1q�6V   }�. 5T   +   ��5U      �� 5V       �� ImageSource$4 ./AtendimentoPDV;component/Img/imagemFundo3.png��6   5X      � 5Z   
   $��0��6   $��2��6   $��1��63   $��2��6@   $��Center=�6Q   $	��Top=�5[      $	��100��6'   .+�5]      �� -   5         ��Name$ UpButton��5]      #��[  6-   $	��Top=�5^      $��Center=�63   �� 5`      -   5         ��Name$ txtQtdPessoas��5`      $��Center=�6-   $��Center=�6H   $	��100��6e   $��30��5a      $��1��6"   $��20(�6+    �� TextAlignment$ Center=�69   �� 5c      -   5        $ 
DownButton��5c      #��[ 6/   $��Bottom=�5d      $��Center=�66   5f      �� 5h   
   -   5         ��Name$
 btOK��5i   g   $��OK��5h      $��30��6   $��Center=�6*   $��	90 0 0 25q�6G   $��Bottom=�6Z   $��0��5i      $��2��6!   $��2��65    J� Opacity$	 0.9Q�6B   $	��100��6u   5j      �� 5l   
   -   5        $��
btCancelar��5m   B   $��Cancelar��5l      $��30��6$   $	 0.9Q�60   $��Center=�6>   $��
0 0 130 25q�6[   $��0��5m      $��2��6!   $��2��6.   $��Bottom=�6k   $	��100��5n      6   5q      5s      