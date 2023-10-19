import React, { useState } from 'react';
import { StyleSheet, TextInput } from 'react-native';

const InputField: React.FC<{ placeholder: string; secureTextEntry?: boolean }> = ({ placeholder, secureTextEntry }) => {
  const [value, setValue] = useState('');

  return (
    <TextInput
      style={styles.input}
      onChangeText={setValue}
      value={value}
      placeholder={placeholder}
      secureTextEntry={secureTextEntry}
    />
  );
};

const styles = StyleSheet.create({
  input: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    marginBottom: 12,
    paddingLeft: 8,
  },
});

export default InputField;