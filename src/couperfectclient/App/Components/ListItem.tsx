import { Text, View } from "react-native";

const ListItem: React.FC<{ text: string }> = ({ text }) => {
  return (
    <View style={{gap: 10, flexDirection: 'row', alignItems: 'center' }}>
      <Text style={anchorStyle}>{'\u2022'}</Text>
      <Text style={anchorStyle}>{text}</Text>
    </View>
  );
};

const anchorStyle = {
  color: '#DAD8D8',
  fontSize: 40,
  textShadowColor: '#0149BF',
  textShadowOffset: {width: -1, height: 1},
  textShadowRadius: 10,
}

export default ListItem;