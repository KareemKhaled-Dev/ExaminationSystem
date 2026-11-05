protected internal
الـ Member يقدر يتستخدم:
من أي Class داخل نفس الـ Assembly (المشروع المترجم).
أو من Class وارث منه (Derived Class) حتى لو في Assembly تانية.
يعني: نفس الـ Assembly أو عن طريق الوراثة.
2. private protected
الـ Member يقدر يتستخدم:
داخل الـ Class نفسه.
أو داخل Class وارث منه، بشرط يكون في نفس الـ Assembly.
يعني: لازم يكون فيه وراثة وفي نفس الـ Assembly.
ممكن نقول 
protected internal = OR
private protected = AND
