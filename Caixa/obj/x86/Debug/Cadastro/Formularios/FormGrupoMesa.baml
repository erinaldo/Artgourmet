   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa6    0Artebit.Restaurante.Caixa.Cadastro.FormGrupoMesa   5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      $	��400��6K   $	��600��6X   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6d   ( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5      
������ $��Grupo de Mesas��6R    � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
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
��Left=�6B   $	��Top=�5   !   $	��120��69   ���#EEEEEE.��5       �  5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      $��3* �6(   	 � ColumnDefinitions	 5*      �� 5+      �� 5,      $��4* �6+   .+�5.      �� $��Descrição:��6    $��1��62   $��Center=�6?   $��Center=�5/       �� 50      $��Qtd Mínima:��6    $��2��63   $��Center=�6@   $��Center=�51       �� 52      $��Qtd Máxima:��6    $��3��63   $��Center=�6@   $��Center=�53       /  )Telerik.Windows.Controls.RadMaskedTextBox  54      -   5        $��txtboxDescricao��54   /   $��1��6F   $��1��6V   $��25��6c   
   MaskType$

 None=�55   /      EmptyContent$ Digite a descrição��6?   $	��400��6a   $
��Left=�6m     56      -   5        $��txtboxqtdMin��56   /   $��1��6C   $��2��6S   $��25��6`   $

 None=�57   /   $  ��6?   $	��400��6O   $
��Left=�6[     58      -   5        $��txtboxqtdMax��58   /   $��1��6C   $��3��6S   $��25��6`   $

 None=�59   /   $  ��6?   $	��400��6O   $
��Left=�6[   5:      5;      
  5<      -   5        $��RadTabAddMesa��5<   W   $��Mesas��6!   $��30��60   $
��Left=�6<   $	��Top=�5=   !   $	��120��69   ���#EEEEEE.��5>        -   5          Name$ 	gridMesas��5>   &   $	��484��69   $	��276��6I   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�5?   &      AutoGenerateColumns .     
IsReadOnly . $	��Top=�6w      ShowGroupPanel .     SelectionMode$ Multiple=�5@   =      Columns 5A      3  -Telerik.Windows.Controls.GridViewSelectColumn  5B      69   1  +Telerik.Windows.Controls.GridViewDataColumn  5C      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Número Mesa��65   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100 5D   5      IsGroupable .    IsFilterable .     DataMemberBinding 5C   J   �� *nuMesa+5D   T     5E      $ Observação��65   $ * 5F   5    .  .   5E   I   �� *
observacao+5F   R   5I      5J      5K      �� 5L   
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   ._�5M      � .+�5N        -   5        $��	btnSalvar��5N   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��5O   $   $��25��6/      BotaoMenor2#��[  6S     5P      -	   5        $��btnCancelar��5P   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�5Q   $   $��80��6@   $��25��6K   #��[  6q   5R      5S      5T      5U      