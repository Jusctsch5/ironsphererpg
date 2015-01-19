// KC:
// used to prevent arithmetic fail in T2
// because a number like 239203902 would be 2.39204e+08

function epochTime()
{
    RubyEval("tsEval '$temp=\x22' + Time.now.to_i.to_s + '\x22;'");
    return $temp;
}
function ruby_greaterThan(%a, %b)
{
    // a greater than b
    RubyEval("tsEval '$temp=\x22' + ("@%a@" > "@%b@").to_s + '\x22;'");
    return $temp $= "true";
}
function ruby_lessThan(%a, %b)
{
    // a less than b
    RubyEval("tsEval '$temp=\x22' + ("@%a@" > "@%b@").to_s + '\x22;'");
    return $temp $= "true";
}
function ruby_add(%a, %b)
{
    // a add b
    RubyEval("tsEval '$temp=\x22' + ("@%a@" + "@%b@").to_s + '\x22;'");
    return $temp;
}
function ruby_sub(%a, %b)
{
    // a sub b
    RubyEval("tsEval '$temp=\x22' + ("@%a@" - "@%b@").to_s + '\x22;'");
    return $temp;
}
