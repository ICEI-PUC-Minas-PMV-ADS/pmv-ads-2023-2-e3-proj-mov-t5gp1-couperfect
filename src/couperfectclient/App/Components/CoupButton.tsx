import React from 'react';
import { Button } from 'react-native';
import { colors } from '../Styles';

interface CoupButtonProps{
  title : string;
  onPress?: () => void;
}

const CoupButton: React.FC<CoupButtonProps> = ({ onPress, title }) => {
  return <Button onPress={onPress} title={title} color={colors.Primary} />;
};

export default CoupButton;