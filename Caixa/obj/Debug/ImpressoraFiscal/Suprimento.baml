   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa;    5Artebit.Restaurante.Caixa.ImpressoraFiscal.Suprimento   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   	   $��
Suprimento��5   	   $	��300��6   $	��474��6*   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�66   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6L   
�������.��5      � .+�5   
   �� -   5        $��label1��5   e   $��Forma Suprimento:��6   $��28��6,   $
��Left=�68   $��3,38,0,0q�6S   $	��Top=�6s   $��14(�6�   * 
 $Telerik.Windows.Controls.RadComboBox  5   
   -   5        $��comboFormaPgto��5   k    �� DisplayMemberPath$ 	descricao��6   $
��Left=�6<   $��
136,38,0,0q�6W    �� SelectedValuePath$ idFormaPGTO��6�   $	��Top=�6�   $	��250��6�   / 
 )Telerik.Windows.Controls.RadNumericUpDown  5	   
   -   5        $��txtValor��5	   r   $
��Left=�6#   $��
136,76,0,0q�6>   +  %Telerik.Windows.Controls.RadRangeBase   Maximum$ 	100000000Q�6R      Minimum$ 0Q�6f      ValueFormat$ Currency=�6�   $	��Top=�6�   $	��250��6�   �� 5
   
   -   5        $��label2��5
   h   $��Valor:��6   $��14(�6!   $��28��6/   $
��Left=�6;   $��3,72,0,0q�6V   $	��Top=�6v   �� 5   
   $��Bottom=�6   $��1��6,   
�������$	��465��6Q   $��50��6]   ._�5      � .+�5      (  "Telerik.Windows.Controls.RadButton  -   5        $��btnOK��5   $   $��OK��61   $��0 0 100 -10q�6>   $��Right=�6S   $��80��6o   $��25��6z      BotaoMenor2#��[  6�     5      -   5        $��btnCancelar��5   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[  6�   5      5      5      5      