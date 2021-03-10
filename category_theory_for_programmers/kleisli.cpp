#include <optional>

std::optional<double> safe_root(double x){
  if(x > 0)
    return x;
  else
    return {};
}

//auto safe_reciprocal(double x){
//  return x != 0 ? std::optional<double>{1.0 / x}: std::nullopt;
//}

