   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa1    +Artebit.Restaurante.Caixa.Fiscal.LeituraMFD   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
      � Title$  Leitura MFD��$	��362��$	��450��.��� .+��� -   $��lbTitulo��$��Geração Espelho MFD��$��28��$
��Left=�$��	12,12,0,0q�$	��Top=��� -   $��
txtArquivo��$��23��$
��Left=�$��	64,47,0,0q�$	��Top=�$	��258���� -   $��
btProcurar��$��Procurar��$��23��$
��Left=�$��
327,47,0,0q�$	��Top=�$��75���� -   $��label2��$��Arquivo:��$��28��$
��Left=�$��	13,45,0,0q�$	��Top=��� -   $��groupPeriodo��$��Período��$��85��$
��Left=�$��
13,176,0,0q�$	��Top=�$	��200��.��� .+�, 
 &Telerik.Windows.Controls.RadDatePicker  -   $��data1��0 
 *Telerik.Windows.Controls.RadDateTimePicker   DateTimeWatermarkContent$  ��$
��Left=�$��35,5,0,0q�$	��Top=�$	��117���� -   $��label3��$	��De:��$��28��$
��Left=�$��0,4,0,0q�$	��Top=��� -   $��label4��$	��à:��$��28��$
��Left=�$��0,28,0,0q�$	��Top=�  -	   $��data2��$  ��$
��Left=�$��	35,32,0,0q�$	��Top=�$	��117���� -
   $
��btOK��$��OK��$��23��$
��Left=�$��247,288,0,0q�$	��Top=�$��75���� -   $��
btCancelar��$��Cancelar��$��23��$��Right=�$��
0,288,18,0q�$	��Top=�$��75���� -   $��
groupFaixa��$��Faixa de Dados��$��86��$
��Left=�$��218,175,0,0q�$	��Top=�$	��198��$y�Visible=�{�.  .��� .+��� -   $��label5��$	��De:��$��28��$
��Left=�$��6,6,0,0q�$	��Top=��� -   $��label6��$	��à:��$��28��$
��Left=�$��6,30,0,0q�$	��Top=�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��reducao1��$
��Left=�$��40,6,0,0q�   Mask$ ######��   MaskType$ Standard=�$	��Top=�$��96��  -   $��reducao2��$
��Left=�$��	40,36,0,0q�$ ######��$	��Top=�$��96���� -   $��	groupBox1��$��Tipo de Dados��$��85��$
��Left=�$��	14,79,0,0q�$	��Top=�$	��142��.��� .+�	� -   $��rbtData��$��Por Data�� 	� 	GroupName$ tipoDado��$��16��$
��Left=� d� 	IsChecked$
 TrueY�$��	14,14,0,0q�$	��Top=�	� -   $��
rbtReducao��$��Por Redução��$ tipoDado��$��16��$
��Left=�$��	14,36,0,0q�$	��Top=�{�.  