   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa?    9Artebit.Restaurante.Caixa.Cadastro.Formularios.FormPerfil   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    $	��300��$	��600��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $��Perfil��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������ .���  � RowDefinitions �� $��* ��� $��50 �����  �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��.+�,  &Telerik.Windows.Controls.RadTabControl  -     Name$ radTabControl1��$��Stretch=�$��0q�$��Stretch=�;  5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�.@�)  #Telerik.Windows.Controls.RadTabItem  -     Name$ RadTabEditar��$��Informações��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��� $��	15 15 0 0q� �� $	��40* ��� $	��40* ��� $	��40* ��� $	��40* � � ColumnDefinitions �� $
��130* ��� $
��366* ��� $	��67* �.+��� $��Nome:��$��0��$��Center=�$��16��$��0,6,0,11q��� $��Sistema:��$��Center=�$��16��$��0,1,0,17q�$��1���� $��Expiração Senha(dias):��$��2��$��Center=�$��16��$��0,1,0,14q�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   	  Name$	 
txtboxNome��$��1��$��0��$��30��$��Center=�
   MaskType$

 None=�   EmptyContent$ Digite o nome��$	��270��$
��Left=�$	��0,4q�   Value �� *	descricao+* 
 $Telerik.Windows.Controls.RadComboBox  -     Name$ 	cbSistema��$��1��$��1�� �� DisplayMemberPath$ 	descricao�� �� SelectedValuePath$ 
codSistema��$��30��$��Center=� �� SelectedValue �� *
codSistema+  -   $	 txtDiasExpira��$��1��$��2��$��30��$��Center=�$

 None=�$	��270��$
��Left=�$	��0,4q� �� *
valSenha+�� -    ��Name$ chAtivo��$��3��$��0��$��3��$��Ativo��$��Center=� d� 	IsChecked �� *ativo+�� $��Bottom=�$��1��
�������$	��600��$��55��._�� .+�(  "Telerik.Windows.Controls.RadButton  -     Name$ 	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $ btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  