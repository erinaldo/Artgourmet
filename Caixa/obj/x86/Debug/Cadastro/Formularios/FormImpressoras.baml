   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa8    2Artebit.Restaurante.Caixa.Cadastro.FormImpressoras   5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      $	��400��6K   $	��600��6X   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6d   ( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5      
������  � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5	      ��  �@ Theme$ Windows7 6;   5
      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � $��1q�6    � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   ��5   
   �� 5       �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��6   .+�5   
   , 	 &Telerik.Windows.Controls.RadTabControl  -   5        $��radTabControl1��5   I   $��Stretch=�6    $��0q�6>   $��Stretch=�5       ;	 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase 	  BackgroundVisibility$ 	Collapsed=�6<   .@�5      )
 	 #Telerik.Windows.Controls.RadTabItem
  -   5        $��RadTabEditar��5   ]   $��Informações��6!   $��30��66   $
��Left=�6B   $	��Top=�5   !   $	��120��69   ���#EEEEEE.��5       �  5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      $��3* �6(   	 � ColumnDefinitions	 5+      �� 5,      �� 5-      $��4* �6+   .+�5/      �� $��Descrição:��6    $��1��62   $��Center=�6?   $��Center=�50       �� 51      $��
Ligado à:��6    $��2��61   $��Center=�6>   $��Center=�52       �� 53      $��Modelo:��6    $��3��6/   $��Center=�6<   $��Center=�6Y   �� 54      $	��IP:��6    $��4��6+   $��Center=�68   $��Center=�6U   �� 55      $��Ativo:��6    $��5��6.   $��Center=�6;   $��Center=�6X   �� 56      $��Imprime Produtos:��6    $��6��69   $��Center=�6F   $��Center=�57       /  )Telerik.Windows.Controls.RadMaskedTextBox  58      -   5        $��txtboxDescricao��58   /   $��1��6F   $��1��6V   $��25��6c   
   MaskType$

 None=�59   /      EmptyContent$ Digite a descrição��6?   $	��400��6a   $
��Left=�6m     5:      -   5        $��txtboxligadoa��5:   /   $��1��6D   $��2��6T   $��25��6a   $

 None=�5;   /   $ Digite a referência��6?   $	��400��6b   $
��Left=�6n     5<      -   5        $��txtboxmodelo��5<   /   $��1��6C   $��3��6S   $��25��6`   $

 None=�5=   /   $ Digite o modelo��6?   $	��400��6^   $
��Left=�6j     5>      -   5        $��txtIpImpressora��5>   /   $��1��6F   $��4��6V   $��25��6c   $

 None=�5?   /   $ Digite o IP��6?   $	��400��6Z   $
��Left=�6f   �� 5@      -   5        $��
checkAtivo��5@   W   $��1��6   $��5��6/   $��Center=�6<   6W   �� 5A      -   5        $��checkProdutos��5A   W   $��1��6   $��6��6/   $��Center=�6<   6W   5B      5C      
  5D      -	   5        $��RadTabAddMesa��5D   W   $��Mesas��6!   $��30��60   $
��Left=�6<   $	��Top=�5E   !   $	��120��69   ���#EEEEEE.��5F        -
   5          Name$ 	gridMesas��5F   &   $	��484��69   $	��276��6I   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�5G   &      AutoGenerateColumns .     
IsReadOnly . $	��Top=�6w      ShowGroupPanel .     SelectionMode$ Multiple=�5H   =      Columns 5I      3  -Telerik.Windows.Controls.GridViewSelectColumn  5J      69   1  +Telerik.Windows.Controls.GridViewDataColumn  5K      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Número Mesa��65   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100 5L   5      IsGroupable .    IsFilterable .     DataMemberBinding 5K   J   �� *nuMesa+5L   T     5M      $ Observação��65   $ * 5N   5    .  .   5M   I   �� *
observacao+5N   R   5Q      5R      
  5S      -   5        $��RadTabAddProduto��5S   Z   $��Produtos��6!   $��30��63   $
��Left=�6?   $	��Top=�5T   !   $	��120��69   ���#EEEEEE.��5U        -   5        $ gridProdutos��5U   &   $	��484��6<   $	��276��6L    .  $ 	Collapsed=�5V   &    .   . $	��Top=�6w    .  $ Multiple=�5W   =    5X        5Y      69     5Z      $ Código��65   $	 100 6j    .  .  6E   �� *codigo+5[   H     5\      $ Produto��65   $ * 6i    .  .  6F   �� *nome+5]   H   5`      5a      5b      �� 5c   
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   ._�5d      � .+�5e        -   5        $��	btnSalvar��5e   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��5f   $   $��25��6/      BotaoMenor2#��[  6S     5g      -   5        $��btnCancelar��5g   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�5h   $   $��80��6@   $��25��6K   #��[  6q   5i      5j      5k      5l      