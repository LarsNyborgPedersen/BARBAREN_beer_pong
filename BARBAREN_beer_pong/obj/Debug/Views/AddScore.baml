   M S B A M L   `   `   `  ����Y  UBARBAREN_beer_pong, Version=1.0.0.0, Culture=neutral, PublicKeyToken=006a8cc6c34053f8C&clr-namespace:BARBAREN_beer_pong.ViewsBARBAREN_beer_pong.Views  5      '    !BARBAREN_beer_pong.Views.AddScore   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:BARBAREN_beer_pong.Views     5      .��5	      �   � RowDefinitions  5
   
   �� 5      �� 5      $
��0.5* �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ teamNameSentence��5      $��Center=�6.   $��Center=�6K   $
��Bold"�6f   $��40(�6x   $��0��6�   $��0��6�   $��7��6�   �� 5   
   $��Vundet��6   $��Center=�6"   $��Bottom=�6?   $
��Bold"�6Z   $��30(�6l   $��1��6z   $��1��6�   �� 5   
   $
��Tabt��6   $��Center=�6    $��Bottom=�6=   $
��Bold"�6X   $��30(�6j   $��1��6x   $��5��6�   �� 5   
   -   5         ��Name$ wonDecrement��5      $��-��6'   $	��100(�63   $
��Bold"�6B   $��2��6o   $��0��6|   �� 5   
   -   5         ��Name$ gamesWon��5      $��2��6G   $��1��6T   $��Center=�6d   $��Center=�6�   $��40(�6�   �� 5    
   -   5        $ wonIncrement��5       $	��100(�6'   $
��Bold"�66   $��+��6H   $��2��6o   $��2��6|   �� 5"   
   -   5        $ lostDecrement��5"      $	��100(�6(   $
��Bold"�67   $��-��6I   $��2��6q   $��4��6~   �� 5#   
   -   5        $ 	gamesLost��5#      $��2��6J   $��5��6W   $��Center=�6g   $��Center=�6�   $��40(�6�   �� 5$   
   -   5        $ lostIncrement��5$      $	��100(�6(   $
��Bold"�67   $��+��6I   $��2��6q   $��6��6~   5&      5'      