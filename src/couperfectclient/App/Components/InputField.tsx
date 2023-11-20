import React, { useState } from 'react';
import { StyleSheet, TextInput, TextStyle } from 'react-native';
import { colors } from '../Styles';

interface InputFieldProp {
  placeholder: string;
  secureTextEntry?: boolean;
  style?: TextStyle;
  onTextChange(e: string): void;
}

const InputField: React.FC<InputFieldProp> = ({
  placeholder,
  secureTextEntry,
  style,
  onTextChange
}) => {
  const [value, setValue] = useState('');

  const styles = StyleSheet.compose(defaultStyle, style);

  return (
    <TextInput
      placeholderTextColor={colors.NeutralContrast}
      style={styles}
      onChangeText={e => {
        setValue(e);
        onTextChange(e);
      }}
      value={value}
      placeholder={placeholder}
      secureTextEntry={secureTextEntry}
    />
  );
};

const defaultStyle = {
  height: 40,
  borderColor: colors.PrimaryContrast,
  borderWidth: 1,
  marginBottom: 12,
  paddingLeft: 8,
  borderRadius: 4,
  color: colors.NeutralContrast,
};

export default InputField;
