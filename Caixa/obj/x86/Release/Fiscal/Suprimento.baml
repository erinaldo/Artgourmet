   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa1    +Artebit.Restaurante.Caixa.Fiscal.Suprimento   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    $��
Suprimento��$	��300��$	��474��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
�������.��� .+��� -   $��label1��$��Forma Suprimento:��$��28��$
��Left=�$��3,38,0,0q�$	��Top=�$��14(�* 
 $Telerik.Windows.Controls.RadComboBox  -   $��comboFormaPgto�� �� DisplayMemberPath$ 	descricao��$
��Left=�$��
136,38,0,0q� �� SelectedValuePath$ idFormaPGTO��$	��Top=�$	��250��/ 
 )Telerik.Windows.Controls.RadNumericUpDown  -   $��txtValor��$
��Left=�$��
136,76,0,0q�+  %Telerik.Windows.Controls.RadRangeBase   Maximum$ 	100000000Q�   Minimum$ 0Q�   ValueFormat$ Currency=�$	��Top=�$	��250���� -   $��label2��$��Valor:��$��14(�$��28��$
��Left=�$��3,72,0,0q�$	��Top=��� $��Bottom=�$��1��
�������$	��465��$��50��._�� .+�(  "Telerik.Windows.Controls.RadButton  -   $��btnOK��$��OK��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  