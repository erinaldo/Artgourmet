   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa1    +Artebit.Restaurante.Caixa.Fiscal.LeituraMFD   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   6     � Title$  Leitura MFD��6	   $	��362��6   $	��450��6*   .��5      � .+�5   
   �� -   5        $��lbTitulo��5   h   $��Geração Espelho MFD��6   $��28��6.   $
��Left=�6:   $��	12,12,0,0q�6U   $	��Top=�6x   �� 5   
   -   5        $��
txtArquivo��5   L   $��23��6   $
��Left=�6   $��	64,47,0,0q�69   $	��Top=�6^   $	��258��6v   �� 5   
   -   5        $��
btProcurar��5   _   $��Procurar��6   $��23��6$   $
��Left=�60   $��
327,47,0,0q�6K   $	��Top=�6q   $��75��6�   6�   �� 5	   
   -   5        $��label2��5	   ]   $��Arquivo:��6   $��28��6#   $
��Left=�6/   $��	13,45,0,0q�6J   $	��Top=�6k   �� 5
   
   -   5        $��groupPeriodo��5
   _   $��Período��6   $��85��6$   $
��Left=�60   $��
13,176,0,0q�6K   $	��Top=�6s   $	��200��6�   .��5      � .+�5      , 
 &Telerik.Windows.Controls.RadDatePicker  -   5        $��data1��5   q   0 
 *Telerik.Windows.Controls.RadDateTimePicker   DateTimeWatermarkContent$  ��6(   $
��Left=�6D   $��35,5,0,0q�6_   $	��Top=�6~   $	��117��6�   �� 5      -   5        $��label3��5   ^   $	��De:��6   $��28��6&   $
��Left=�62   $��0,4,0,0q�6M   $	��Top=�6l   �� 5      -   5        $��label4��5   ^   $	��à:��6   $��28��6%   $
��Left=�61   $��0,28,0,0q�6L   $	��Top=�6l     5      -	   5        $��data2��5   r   $  ��6(   $
��Left=�6D   $��	35,32,0,0q�6_   $	��Top=�6   $	��117��6�   5      5      �� 5   
   -
   5        $
��btOK��5   Z   $��OK��6   $��23��6   $
��Left=�6*   $��247,288,0,0q�6E   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5        $��
btCancelar��5   `   $��Cancelar��6   $��23��6$   $��Right=�60   $��
0,288,18,0q�6L   $	��Top=�6r   $��75��6�   6�   �� 5   
   -   5        $��
groupFaixa��5   g   $��Faixa de Dados��6   $��86��6+   $
��Left=�67   $��218,175,0,0q�6R   $	��Top=�6y   $	��198��6�   $y�Visible=�6�   {�.  .��5      � .+�5      �� -   5        $��label5��5   ^   $	��De:��6   $��28��6&   $
��Left=�62   $��6,6,0,0q�6M   $	��Top=�6l   �� 5      -   5        $��label6��5   ^   $	��à:��6   $��28��6%   $
��Left=�61   $��6,30,0,0q�6L   $	��Top=�6l   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5      -   5        $��reducao1��5   z   $
��Left=�6+   $��40,6,0,0q�6F      Mask$ ######��6X      MaskType$ Standard=�6f   $	��Top=�6�   $��96��6�     5      -   5        $��reducao2��5   g   $
��Left=�6+   $��	40,36,0,0q�6F   $ ######��6Y   $	��Top=�6w   $��96��6�   5      5      �� 5   
   -   5        $��	groupBox1��5   d   $��Tipo de Dados��6   $��85��6*   $
��Left=�66   $��	14,79,0,0q�6Q   $	��Top=�6u   $	��142��6�   .��5      � .+�5      	� -   5        $��rbtData��5   �   $��Por Data��6    	� 	GroupName$ tipoDado��61   $��16��6F   $
��Left=�6R    d� 	IsChecked$
 TrueY�6m   $��	14,14,0,0q�6~   $	��Top=�6�   6�   	� 5      -   5        $��
rbtReducao��5   �   $��Por Redução��6   $ tipoDado��64   $��16��6I   $
��Left=�6U   $��	14,36,0,0q�6p   $	��Top=�6�   {�.  6�   5       5!      5"      5#      