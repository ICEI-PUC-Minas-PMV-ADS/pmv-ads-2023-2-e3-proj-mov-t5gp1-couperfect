import {Pressable, StyleSheet, ViewStyle} from 'react-native';
import ArrowReturnLeftSvg from '../Assets/ArrowReturnLeft.svg';
import colors from '../Styles';
import React from 'react';

interface BackButtonProps {
  style?: ViewStyle
}

const BackButton: React.FC<BackButtonProps> = ({ style }) => {
  const defaultStyle: ViewStyle = {
    width: 40,
    height: 40,
    backgroundColor: colors.PrimaryContrast,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: colors.NeutralContrast,
    justifyContent: 'center',
    alignItems: 'center',
  };
  
  const combineStyle = StyleSheet.compose(defaultStyle, style);
  return (
    <Pressable style={combineStyle}>
      <ArrowReturnLeftSvg />
    </Pressable>
  );
}

export default BackButton;