   M S B A M L   `   `   `  ����@  <VKbrd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,"clr-namespace:VKbrd;assembly=VKbrdVKbrd  5   [    AtendimentoPDV9   3Artebit.Restaurante.AtendimentoPDV.Telas.JuntarMesa   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
   5      Fd2http://schemas.microsoft.com/expression/blend/2008    	 
   5   J   Pmc;http://schemas.openxmlformats.org/markup-compatibility/2006    	 
   5      :VKbrd"clr-namespace:VKbrd;assembly=VKbrd    	 
   6[   $��Juntar Mesa��5   V   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6k   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5      $	��360��5      $	��430��6    
������  +� FocusedElement 6F   ��  �� ElementName$ txt_mesa��.��5
      � -   5        $��janela��5
      $	��360��6   $	��430��6%   $��Center=�62   $��Center=�6O    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      �� 5      .+�5   
   ��  �� CornerRadius$ 5��6   ���#49181A$��1q�68   $��-2q�6L   $��2��6X   $��3��5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6#   	   
BlurRadius$	 10Q�63   ._�5!      �� ._�5#      �� 
������ $��0q�63   
������ 
 �� Padding$
 0q�6`   $	��260��6l   $	��330��5$      ._�5&      � .+�5'         VKbrd.KeyPad2  -   5          Name$ keyPad1��5'   E   $��Center=�6(   $	��Top=�6V   $	��260��5(   (   $	��235��65   $��0 50 0 0q�6A   �� 5)      -   5        $��txt_mesa��5)   \   $��40��6"   $��Center=�6.   $��0 0 0 0q�6K   $��18(�5*   "   $	��Top=�60   $	��235��6H   6T   5+      5,      5-      ��5.      �� 5/       �� ImageSource$4 ./AtendimentoPDV;component/Img/imagemFundo3.png��6   51      �� 54   
   -   5        $��
btCancelar��55   B   $��Cancelar��54      $��35��6$    J� Opacity$	 0.9Q�60   $��Center=�6>   $��-20 0 100 25q�6[   $��0��55      $��2��6!   $��2��6.   $��Bottom=�6k   $	��100��56      6   �� 58   
   -   5        $��	btSeparar��59   B   $��Separar��58      $��35��6#   $	 0.9Q�6/   $��Center=�6=   $��
120 0 0 25q�6Z   $��0��59      $��2��6!   $��2��6.   $��Bottom=�6j   $	��100��5:      6   5<      5=      