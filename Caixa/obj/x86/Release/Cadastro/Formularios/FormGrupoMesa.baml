   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa6    0Artebit.Restaurante.Caixa.Cadastro.FormGrupoMesa   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   $	��400��$	��600��) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������ $��Grupo de Mesas�� � 	Resources (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value-  'Telerik.Windows.Controls.ThemeConverter*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%�   '        '  /     '  [     �� "  .���  �@ Theme$ Windows7 �� "  .���  �@ Theme$ Metro �� "  .���  �@ Theme$ Summer .��� $��1q� � RowDefinitions �� �� $��50 �����  �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��.+�, 	 &Telerik.Windows.Controls.RadTabControl  -   $��radTabControl1��$��Stretch=�$��0q�$��Stretch=�;	 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase 	  BackgroundVisibility$ 	Collapsed=�.@�)
 	 #Telerik.Windows.Controls.RadTabItem
  -   $��RadTabEditar��$��Informações��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.���  �� �� �� �� �� �� �� $��3* �	 � ColumnDefinitions	 �� �� $��4* �.+��� $��Descrição:��$��1��$��Center=�$��Center=��� $��Qtd Mínima:��$��2��$��Center=�$��Center=��� $��Qtd Máxima:��$��3��$��Center=�$��Center=�/  )Telerik.Windows.Controls.RadMaskedTextBox  -   $��txtboxDescricao��$��1��$��1��$��25��
   MaskType$

 None=�   EmptyContent$ Digite a descrição��$	��400��$
��Left=�  -   $��txtboxqtdMin��$��1��$��2��$��25��$

 None=�$  ��$	��400��$
��Left=�  -   $��txtboxqtdMax��$��1��$��3��$��25��$

 None=�$  ��$	��400��$
��Left=�
  -   $��RadTabAddMesa��$��Mesas��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��  -     Name$ 	gridMesas��$	��484��$	��276��;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�   AutoGenerateColumns .     
IsReadOnly . $	��Top=�   ShowGroupPanel .     SelectionMode$ Multiple=�   Columns 3  -Telerik.Windows.Controls.GridViewSelectColumn  1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Número Mesa��-  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100    IsGroupable .    IsFilterable .     DataMemberBinding �� *nuMesa+  $ Observação��$ *  .  .   �� *
observacao+�� $��Bottom=�$��1��
�������$	��590��$��55��._�� .+�  -   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -	   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  