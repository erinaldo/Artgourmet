   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa<    6Artebit.Restaurante.Caixa.Cadastro.FormTipoRecebimento   5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5   	   $	��400��5   	   $	��600��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   	   $��Tipos de Recebimento��6   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   
�������&   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5   	    � 	Resources 5
      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � $��1q�6    � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   .+�5      �  5      �� 5      �� 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$      $��3* �6(    � ColumnDefinitions 5&      �� 5'      �� 5(      $��4* �6+   .+�5*      �� $��Descrição:��6    $��1��62   $��Center=�6?   $��Center=�6\   �� 5+      $��Ativo:��6   $��2��6&   $��Center=�63   $��Center=�6P   $��
44,10,45,9q�6k   /	  )Telerik.Windows.Controls.RadMaskedTextBox	  5,      -   5        $��txtboxDescricao��5,   /   $��1��6F   $��1��6V   $��25��6c   	 	  MaskType$
	 None=�6o   
 	  EmptyContent$
 Digite a descrição��6   $	��400��6�   $
��Left=�6�   �� 5-      -   5        $��
checkAtivo��5-   O   $��1��6   $��2��6'   $��Center=�64   $��0,12,0,9q�6a   �� 5/      $��Tipo:��6   $
��Left=�6,   $��	44,10,0,0q�6G   $��3��6Z   $	��Top=�6g   �� 50      -   5         ��Name$ cbTipoPagamento��50      $��1��68    �� DisplayMemberPath$ 	descricao��6H   $
��Left=�6f   $��25��6�   $��0,10,0,0q�6�   $��3��6�   $	��Top=�6�   $	��253��6�    �� SelectedValuePath$
 tipo��6�   51      �� 53   
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   ._�54      � .+�55        -   5        $��	btnSalvar��55   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�      BotaoMenor2#��[  6�     56      -   5        $��btnCancelar��56   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[  6�   57      58      59      5:      