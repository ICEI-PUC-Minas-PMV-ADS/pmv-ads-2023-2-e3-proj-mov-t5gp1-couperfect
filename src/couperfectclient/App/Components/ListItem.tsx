import { Text, View } from "react-native";
import { contrastTextStyle } from '../Styles'

const ListItem: React.FC<{ text: string }> = ({ text }) => {
  return (
    <View style={{gap: 10, flexDirection: 'row', alignItems: 'center' }}>
      <Text style={contrastTextStyle}>{'\u2022'}</Text>
      <Text style={contrastTextStyle}>{text}</Text>
    </View>
  );
};

export default ListItem;