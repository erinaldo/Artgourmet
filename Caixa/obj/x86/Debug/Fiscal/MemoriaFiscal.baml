   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa4    .Artebit.Restaurante.Caixa.Fiscal.MemoriaFiscal   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   	     � Title$  Memória Fiscal��5   @   $	��262��6W   $	��420��6d   .��5      � .+�5   
   �� -   5        $��	groupBox1��5   d   $��Tipo de Dados��6   $��85��6*   $
��Left=�66   $��	12,12,0,0q�6Q   $	��Top=�5      $	��142��6+   .��5      � .+�5	      	� -   5        $��rbtData��5
      $��Por Data��5	      $��16��61   $
��Left=�6=   $��	14,14,0,0q�6X   $	��Top=�5
   -    	� 	GroupName$ tipoDado��6E    d� 	IsChecked$
 TrueY�6Z   	� 5      -   5        $��
rbtReducao��5      $��Por CRZ��5      $��16��60   $
��Left=�6<   $��	14,36,0,0q�6W   $	��Top=�5   0   $ tipoDado��6H   5      5      �� 5   
   -   5        $��	groupBox2��5   _   $��Período��6   $��85��6$   $
��Left=�60   $��
192,12,0,0q�6K   $	��Top=�5      $	��200��6+   .��5      � .+�5      , 
 &Telerik.Windows.Controls.RadDatePicker  -   5        $��data1��5   (   0 
 *Telerik.Windows.Controls.RadDateTimePicker   DateTimeWatermarkContent$  ��5   (   $
��Left=�6D   $��35,5,0,0q�6_   $	��Top=�5   5   $	��117��6M   �� 5      -   5        $��label1��5   ^   $	��De:��6   $��28��6&   $
��Left=�62   $��0,4,0,0q�6M   $	��Top=�5      �� 5      -   5        $��label2��5   ^   $	��à:��6   $��28��6%   $
��Left=�61   $��0,28,0,0q�6L   $	��Top=�5        5      -   5        $��data2��5   (   $  ��5   (   $
��Left=�6D   $��	35,32,0,0q�6_   $	��Top=�5   5   $	��117��6M   5      5      �� 5   
   -	   5        $��	groupBox3��5   f   $��Faixa de Dados��6   $��86��6+   $
��Left=�67   $��
192,97,0,0q�6R   $	��Top=�5      $	��198��6+   .��5      � .+�5      �� -
   5        $��label3��5   ^   $	��De:��6   $��28��6&   $
��Left=�62   $��6,6,0,0q�6M   $	��Top=�5       �� 5!      -   5        $��label4��5!   ^   $	��à:��6   $��28��6%   $
��Left=�61   $��6,30,0,0q�6L   $	��Top=�5"      / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5#      -   5        $��reducao1��5#   X   $
��Left=�6+   $��40,6,0,0q�6F   $	��Top=�5$   +   $��96��6C      Mask$
 ####��6N      MaskType$ Standard=�6Z     5%      -   5        $��reducao2��5%   Y   $
��Left=�6+   $��	40,36,0,0q�6F   $	��Top=�5&   +   $��96��6C   $
 ####��6N   5'      5(      �� 5)   
   -   5        $
��btOK��5)   Z   $��OK��6   $��23��6   $
��Left=�6*   $��103,191,0,0q�6E   $	��Top=�5*      $��75��6)   64   �� 5+   
   -   5        $��
btCancelar��5+   `   $��Cancelar��6   $��23��6$   $
��Left=�60   $��204,191,0,0q�6K   $	��Top=�5,      $��75��6)   64   �� 5-   
   -   5        $��
chkArquivo��5.      $��Imprimir em Arquivo��5-      $��16��61   $
��Left=�6=   $��
18,114,0,0q�6X   $	��Top=�5.   %   5/      50      